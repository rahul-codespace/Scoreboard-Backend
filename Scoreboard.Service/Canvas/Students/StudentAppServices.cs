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

        public async Task<Student> CreateStudentAsync(int studentId)
        {

            // Fetch user information asynchronously.
            var userResponse = await GetCanvasApiResponseAsync($"users/{studentId}");

            if (userResponse == null || !userResponse.IsSuccessStatusCode)
            {
                string message = $"Error getting user {studentId} from Canvas API: {(userResponse != null ? userResponse.StatusCode.ToString() : "Unknown")}";
                _logger.LogError(message);
                return null;
            }

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var student = JsonConvert.DeserializeObject<Student>(userContent);

            if (student != null)
            {
                student.StreamId = 1;
                await _studentRepository.AddStudentAsync(student);
                var courseApiResponse = await GetCoursesAsync(student.Id);
                await _courseRepository.AddCourseListAsync(courseApiResponse.Select(c => new Course { Id = c.Id, Name = c.Name }).ToList());
                await _studentAssessmentRepository.DeleteAllStudentAssissmentRecordAsync(student.Id);
                foreach (var course in courseApiResponse)
                {
                    // Fetch assignments asynchronously.
                    var assessments = await GetAssessmentsAsync(course.Id, student.Id);
                    await _assessmentRepository.AddAssessmentListAsync(assessments.Select(a => new Assessment { Id = a.Id, Name = a.Name, Point = a.Points_possible, CourseId = a.Course_id }).ToList());

                    // Fetch grades for each assignment asynchronously.
                    foreach (var assignment in assessments)
                    {
                        var studentAssissment = await GetAssessmentGradeAsync(course.Id, assignment.Id, student.Id);
                        await _studentAssessmentRepository.AddStudentAssessmentAsync(new StudentAssessment {Id = assignment.Id, AssessmentId = assignment.Id, StudentId = student.Id, AchievedPoints = studentAssissment.Score});
                    }
                }
                await _studentTotalPointRepository.AddStudentTotalPointAsync(student.Id);
            }

            return student;
        }

        private async Task<HttpResponseMessage> GetCanvasApiResponseAsync(string endpoint)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
            return await client.GetAsync($"{_canvasApiUrl}/{endpoint}");
        }

        private async Task<List<CourseApiResponseDto>> GetCoursesAsync(int userId)
        {
            var courseResponse = await GetCanvasApiResponseAsync($"users/{userId}/courses");

            if (courseResponse != null && courseResponse.IsSuccessStatusCode)
            {
                var courseContent = await courseResponse.Content.ReadAsStringAsync();
                var courseApiResponse = JsonConvert.DeserializeObject<List<CourseApiResponseDto>>(courseContent);
                return courseApiResponse;
            }

            return new List<CourseApiResponseDto>();
        }

        private async Task<List<AssessmentApiResponseDto>> GetAssessmentsAsync(int courseId, int studentId)
        {
            var assignmentResponse = await GetCanvasApiResponseAsync($"courses/{courseId}/assignments");

            if (assignmentResponse != null && assignmentResponse.IsSuccessStatusCode)
            {
                var assignmentContent = await assignmentResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AssessmentApiResponseDto>>(assignmentContent);
            }

            return new List<AssessmentApiResponseDto>();
        }

        private async Task<StudentAssessmentApiResponseDto> GetAssessmentGradeAsync(int courseId, int assignmentId, int studentId)
        {
            var gradeResponse = await GetCanvasApiResponseAsync($"courses/{courseId}/assignments/{assignmentId}/submissions/{studentId}");

            if (gradeResponse != null && gradeResponse.IsSuccessStatusCode)
            {
                var gradeContent = await gradeResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StudentAssessmentApiResponseDto>(gradeContent);
            }
            return null;
        }

    }
}
