using Microsoft.Extensions.Logging;
using Scoreboard.Contracts.Students;
using Scoreboard.Repository.Assessments;
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


        public StudentAppServices(
            IGetStudentDataServices getStudentDataServices,
            ILogger<StudentAppServices> logger, 
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IStudentAssessmentRepository studentAssessmentRepository,
            IAssessmentRepository assessmentRepository,
            IStudentTotalPointRepository studentTotalPointRepository,
            IStreamCoursesRepository streamCoursesRepository,
            ISubmissionCommentRepository submissionCommentRepository
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
    }
}
