using Microsoft.EntityFrameworkCore;
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

        public async Task<Student> RegisterStudent(RegisterStudentDto input)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var canvasStudent = await _getStudentDataServices.GetStudentData(input.Id);
                    if (canvasStudent == null)
                    {
                        return null;
                    }

                    var student = new Student
                    {
                        Id = canvasStudent.Id,
                        Name = canvasStudent.Name,
                        StreamId = input.StreamId
                    };

                    await _studentRepository.AddStudentAsync(student);

                    //var user = new ScoreboardUser
                    //{
                    //    Name = input.Name,
                    //    Email = canvasStudent.Email,
                    //    UserName = canvasStudent.Email,
                    //};

                    //await _authRepository.Register(user, input.Password);
                    //await _authRepository.AddRoleToUser(user, "Student");

                    dbContextTransaction.Commit(); // Commit the transaction if all operations were successful

                    return student;
                }
                catch (Exception ex)
                {
                    // Handle the exception or log it as needed
                    dbContextTransaction.Rollback(); // Rollback the transaction in case of an error
                    return null;
                }
            }
        }

    }
}
