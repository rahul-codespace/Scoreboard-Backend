using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scoreboard.Contracts.Canvas.ResponseDto;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Assessments;
using Scoreboard.Repository.Courses;
using Scoreboard.Repository.StudentAssessments;
using Scoreboard.Repository.Students;
using Scoreboard.Repository.StudentTotalPoints;
using System.Net.Http.Headers;

namespace Scoreboard.Service.Canvas.Students
{
    public class StudentAppServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StudentAppServices> _logger;
        private readonly string _canvasApiUrl;
        private readonly string _canvasApiToken;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAssessmentRepository _assessmentRepository;
        public readonly IStudentAssessmentRepository _studentAssessmentRepository;
        public readonly IStudentTotalPointRepository _studentTotalPointRepository;


        private readonly HttpClient client = new HttpClient();

        public StudentAppServices(
            IConfiguration configuration, 
            ILogger<StudentAppServices> logger, 
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IStudentAssessmentRepository studentAssessmentRepository,
            IAssessmentRepository assessmentRepository,
            IStudentTotalPointRepository studentTotalPointRepository
            )
        {
            _logger = logger;
            _configuration = configuration;
            _canvasApiUrl = _configuration["Canvas:ApiUrl"]!;
            _canvasApiToken = _configuration["Canvas:ApiKey"]!;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentAssessmentRepository = studentAssessmentRepository;
            _assessmentRepository = assessmentRepository;
            _studentTotalPointRepository = studentTotalPointRepository;
        }

        public async Task<Student> SeedStudentDataAsync(int studentId)
        {
            var student = await GetFromCanvasApiAsync<Student>($"users/{studentId}");
            if (student == null) return null;

            student.StreamId = 1;
            await _studentRepository.AddStudentAsync(student);

            var courses = await GetFromCanvasApiAsync<List<CourseApiResponseDto>>($"users/{student.Id}/courses");
            if (courses == null) return student;

            await _courseRepository.AddCourseListAsync(courses.Select(c => new Course { Id = c.Id, Name = c.Name }).ToList());
            await _studentAssessmentRepository.DeleteAllStudentAssissmentRecordAsync(student.Id);

            foreach (var course in courses)
            {
                var assessments = await GetFromCanvasApiAsync<List<AssessmentApiResponseDto>>($"courses/{course.Id}/assignments");
                if (assessments == null) continue;

                await _assessmentRepository.AddAssessmentListAsync(
                    assessments.Select(a => new Assessment { 
                        Id = a.Id, 
                        Name = a.Name, 
                        Point = a.Points_possible, 
                        CourseId = a.Course_id 
                    }).ToList());

                foreach (var assignment in assessments)
                {
                    var studentAssessment = await GetFromCanvasApiAsync<StudentAssessmentApiResponseDto>($"courses/{course.Id}/assignments/{assignment.Id}/submissions/{student.Id}");
                    if (studentAssessment == null) continue;

                    await _studentAssessmentRepository.AddStudentAssessmentAsync(
                        new StudentAssessment { 
                            Id = assignment.Id, 
                            AssessmentId = assignment.Id, 
                            StudentId = student.Id, 
                            AchievedPoints = studentAssessment.Score 
                        });
                }
            }

            await _studentTotalPointRepository.AddStudentTotalPointAsync(student.Id);

            return student;
        }


        private async Task<T> GetFromCanvasApiAsync<T>(string endpoint)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
            var response = await client.GetAsync($"{_canvasApiUrl}/{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            _logger.LogError($"Error getting data from Canvas API for endpoint {endpoint}: {response.StatusCode}");
            return default;
        }
    }
}
