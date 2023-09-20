using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scoreboard.Contracts.Canvas.ResponseDto;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Assessments;
using Scoreboard.Repository.Courses;
using Scoreboard.Repository.StudentAssessments;
using Scoreboard.Repository.Students;
using Scoreboard.Repository.StudentTotalPoints;
using System.Net.Http.Headers;

namespace Scoreboard.Service.Canvas.Students
{
    public class StudentAppServices : IStudentAppServices
    {
        private int executionCount = 0;
        private readonly ILogger<StudentAppServices> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAssessmentRepository _assessmentRepository;
        public readonly IStudentAssessmentRepository _studentAssessmentRepository;
        public readonly IStudentTotalPointRepository _studentTotalPointRepository;
        public readonly IGetStudentDataServices _getStudentDataServices;

        public StudentAppServices(
            IGetStudentDataServices getStudentDataServices,
            ILogger<StudentAppServices> logger, 
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IStudentAssessmentRepository studentAssessmentRepository,
            IAssessmentRepository assessmentRepository,
            IStudentTotalPointRepository studentTotalPointRepository
            )
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentAssessmentRepository = studentAssessmentRepository;
            _assessmentRepository = assessmentRepository;
            _studentTotalPointRepository = studentTotalPointRepository;
            _getStudentDataServices = getStudentDataServices;
        }

        public async Task SeedData(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);

                var studentIds = await _studentRepository.GetStudentsIds();
                var students = await _getStudentDataServices.SeedStudentsDataAsync(studentIds);
                await SeedStudentDataInDatabaseAsync(students);

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }

        public async Task<List<StudentDto>> SeedStudentDataInDatabaseAsync(List<StudentDto> students)
        {
            await _studentRepository.RemoveTableDataAsync(); // removeing courses and student total points
            foreach (var student in students)
            {
                try
                {
                    await _courseRepository.AddCourseListAsync(student.Courses);
                    await _assessmentRepository.AddAssessmentListAsync(student.Assessments);
                    await _studentAssessmentRepository.AddStudentAssessmentsAsync(student.StudentAssessments);
                    await _studentTotalPointRepository.AddStudentTotalPointAsync(student.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error seeding student data in database for student {student.Id}: {ex.Message}");
                }
            }
            return students;
        }
    }
}
