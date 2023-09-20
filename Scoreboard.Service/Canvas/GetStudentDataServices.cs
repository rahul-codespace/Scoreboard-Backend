using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scoreboard.Contracts.Canvas.ResponseDto;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using System.Net.Http.Headers;

namespace Scoreboard.Service.Canvas
{
    public class GetStudentDataServices : IGetStudentDataServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetStudentDataServices> _logger;
        private readonly string _canvasApiUrl;
        private readonly string _canvasApiToken;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly HttpClient client = new HttpClient();

        public GetStudentDataServices(
            IConfiguration configuration,
            ILogger<GetStudentDataServices> logger
        )
        {
            _logger = logger;
            _configuration = configuration;
            _canvasApiUrl = _configuration["Canvas:ApiUrl"]!;
            _canvasApiToken = _configuration["Canvas:ApiKey"]!;
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
