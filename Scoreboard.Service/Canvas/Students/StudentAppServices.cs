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
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

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
                            AssessmentId = assignment.Id, 
                            StudentId = student.Id, 
                            AchievedPoints = studentAssessment.Score 
                        });
                }
            }

            await _studentTotalPointRepository.AddStudentTotalPointAsync(student.Id);
            return student;
        }

        public async Task<List<StudentDto>> SeedStudentsDataAsync(List<int> studentIds)
        {
            await _semaphore.WaitAsync();
            try
            {
                var studentTasks = studentIds.Select(async studentId =>
                {
                    var student = await GetFromCanvasApiAsync<StudentDto>($"users/{studentId}");
                    if (student != null)
                    {
                        var courses = await GetFromCanvasApiAsync<List<CourseApiResponseDto>>($"users/{student.Id}/courses");
                        if (courses != null)
                        {
                            student.Assessments ??= new List<Assessment>();
                            student.Courses = courses.Select(c => new Course { Id = c.Id, Name = c.Name }).ToList();
                            var assessmentTasks = courses.Select(async course =>
                            {
                                var assessments = await GetFromCanvasApiAsync<List<AssessmentApiResponseDto>>($"courses/{course.Id}/assignments");
                                if (assessments != null)
                                {
                                    student.Assessments.AddRange(assessments.Select(a => new Assessment
                                    {
                                        Id = a.Id,
                                        Name = a.Name,
                                        Point = a.Points_possible,
                                        CourseId = a.Course_id
                                    }));
                                    foreach (var assignment in assessments)
                                    {
                                        var studentAssessment = await GetFromCanvasApiAsync<StudentAssessmentApiResponseDto>($"courses/{course.Id}/assignments/{assignment.Id}/submissions/{student.Id}");
                                        if (studentAssessment != null)
                                        {
                                            student.StudentAssessments ??= new List<StudentAssessment>();
                                            student.StudentAssessments.Add(new StudentAssessment
                                            {
                                                AssessmentId = assignment.Id,
                                                StudentId = student.Id,
                                                AchievedPoints = studentAssessment.Score
                                            });
                                        }
                                    }
                                }
                            });
                            await Task.WhenAll(assessmentTasks);
                        }
                        return student;
                    }
                    return null;
                });

                var students = await Task.WhenAll(studentTasks);
                return students.Where(student => student != null).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async Task<List<StudentDto>> SeedStudentDataInDatabaseAsync(List<StudentDto> students)
        {
            foreach (var student in students)
            {
                await _courseRepository.AddCourseListAsync(student.Courses);
                await _studentAssessmentRepository.DeleteAllStudentAssissmentRecordAsync(student.Id);
                await _assessmentRepository.AddAssessmentListAsync(student.Assessments);
                await _studentAssessmentRepository.AddStudentAssessmentsAsync(student.StudentAssessments);
                await _studentTotalPointRepository.AddStudentTotalPointAsync(student.Id);
            }
            return students;
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
