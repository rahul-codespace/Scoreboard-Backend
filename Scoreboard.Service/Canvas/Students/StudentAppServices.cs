using Microsoft.Extensions.Logging;
using Scoreboard.Contracts.Students;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Assessments;
using Scoreboard.Repository.Auths;
using Scoreboard.Repository.Courses;
using Scoreboard.Repository.StreamCourses;
using Scoreboard.Repository.StudentAssessments;
using Scoreboard.Repository.Students;
using Scoreboard.Repository.StudentTotalPoints;
using Scoreboard.Repository.SubmissionComments;
using Scoreboard.Service.Email;

namespace Scoreboard.Service.Canvas.Students
{
    public class StudentAppServices : IStudentAppServices
    {
        private int executionCount = 0;
        private readonly ILogger<StudentAppServices> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IStudentAssessmentRepository _studentAssessmentRepository;
        private readonly IStudentTotalPointRepository _studentTotalPointRepository;
        private readonly IGetStudentDataServices _getStudentDataServices;
        private readonly IStreamCoursesRepository _streamCoursesRepository;
        private readonly ISubmissionCommentRepository _submissionCommentRepository;
        private readonly IEmailServices _emailServices;
        private readonly IAuthRepository _authRepository;
        private readonly ScoreboardDbContext _context;


        public StudentAppServices(
            IGetStudentDataServices getStudentDataServices,
            ILogger<StudentAppServices> logger, 
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IStudentAssessmentRepository studentAssessmentRepository,
            IAssessmentRepository assessmentRepository,
            IStudentTotalPointRepository studentTotalPointRepository,
            IStreamCoursesRepository streamCoursesRepository,
            ISubmissionCommentRepository submissionCommentRepository,
            IAuthRepository authRepository,
            IEmailServices emailServices,
            ScoreboardDbContext context
            )
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentAssessmentRepository = studentAssessmentRepository;
            _assessmentRepository = assessmentRepository;
            _studentTotalPointRepository = studentTotalPointRepository;
            _getStudentDataServices = getStudentDataServices;
            _streamCoursesRepository = streamCoursesRepository;
            _submissionCommentRepository = submissionCommentRepository;
            _authRepository = authRepository;
            _context = context;
            _emailServices = emailServices;
        }

        public async Task SeedData(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);

                var studentData = await _studentRepository.GetStudentsAsync();
                var students = await _getStudentDataServices.GetStudentsDataFromCanvas(studentData);
                await SeedStudentDataInDatabaseAsync(students);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        public async Task SeedStudentDataInDatabaseAsync(List<StudentDto> students)
        {
            await _studentRepository.RemoveTableDataAsync(); // removeing courses and student total points
            foreach (var student in students)
            {
                try
                {
                    await _courseRepository.AddListAsync(student.Courses);
                    await _streamCoursesRepository.AddListAsync(student.StreamId, student.Courses);
                    await _assessmentRepository.AddListAsync(student.Assessments);
                    await _studentAssessmentRepository.AddListAsync(student.StudentAssessments);
                    await _submissionCommentRepository.AddListAsync(student.SubmissionComments);
                    await _studentTotalPointRepository.AddStudentTotalPointAsync(student.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error seeding student data in database for student {student.Id}: {ex.Message}");
                }
            }
        }

        public async Task<object> RegisterStudent(RegisterStudentDto input)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Step 1: Get student data from Canvas
                    var canvasStudentList = await _getStudentDataServices.GetStudentData(input.Email);
                    if (canvasStudentList == null)
                    {
                        throw new Exception("Student not found in Canvas");
                    }

                    // Step 2: Check if the user already exists
                    var canvasStudent = canvasStudentList[0];
                    var existingUser = await _authRepository.GetUser(canvasStudent.Email);
                    if (existingUser != null)
                    {
                        throw new Exception("User already exists. Please login with your Canvas registered email Id");
                    }

                    // Step 3: Create a new ScoreboardUser and register it
                    var user = new ScoreboardUser
                    {
                        Name = canvasStudent.Name,
                        Email = canvasStudent.Email,
                        UserName = canvasStudent.Email,
                    };

                    await _authRepository.Register(user, input.Password);
                    await _authRepository.AddRoleToUser(user, "Student");

                    // Step 4: Create a new Student and add it to the repository
                    var student = new Student
                    {
                        Id = canvasStudent.Id,
                        Name = canvasStudent.Name,
                        StreamId = input.StreamId,
                        Email = canvasStudent.Email,
                    };

                    await _studentRepository.AddStudentAsync(student);

                    //// Step 5: Send a welcome email
                    //string subject = "Welcome to Scoreboard Platform!";
                    //string recipientEmail = user.Email;
                    //string recipientName = user.Name;
                    //string companyName = "Promact";

                    //string messageBody = $"Dear {recipientName},<br><br>" +
                    //    "Welcome to Scoreboard Platform! We're thrilled to have you on board.<br><br>" +
                    //    "Please login with your Canvas registered email address, and your password will remain the same as the one you provided during registration.<br><br>" +
                    //    $"Thanks for choosing Scoreboard Platform, brought to you by {companyName}!<br><br>" +
                    //    "Best regards,<br>" +
                    //    "The Scoreboard Platform Team";

                    //_emailServices.SendEmail(new MessageDto(new List<string> { recipientEmail }, subject, messageBody));

                    // Step 6: Commit the database transaction if all operations were successful
                    dbContextTransaction.Commit();

                    return student;
                }
                catch (Exception ex)
                {
                    // Step 7: Handle the exception or log it as needed
                    dbContextTransaction.Rollback(); // Rollback the transaction in case of an error
                    var errorObject = new
                    {
                        IsError = true,
                        ErrorMessage = ex.Message
                    };

                    return errorObject;
                }
            }
        }

    }
}
