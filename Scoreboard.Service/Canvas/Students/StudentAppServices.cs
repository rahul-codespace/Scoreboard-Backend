

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;
using System.Net.Http.Headers;

namespace Scoreboard.Service.Canvas.Students
{
    public class StudentAppServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StudentAppServices> _logger;
        private readonly string _canvasApiUrl;
        private readonly string _canvasApiToken;
        private readonly StudentRepository _studentRepository;
        private readonly HttpClient client = new HttpClient();

        public StudentAppServices(IConfiguration configuration, ILogger<StudentAppServices> logger, StudentRepository studentRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _canvasApiUrl = _configuration["Canvas:ApiUrl"]!;
            _canvasApiToken = _configuration["Canvas:ApiKey"]!;
            _studentRepository = studentRepository;
        }

        public async Task<Student> CreateStudentAsync(int userId)
        {
            var courses = new List<Course>();
            var assigments = new List<Assessment>();
    
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
            var response = await client.GetAsync($"{_canvasApiUrl}/users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<Student>(content);
                
                if (student != null)
                {
                    student.StreamId = 1;
                    var courseResponse = await client.GetAsync($"{_canvasApiUrl}/users/{student.Id}/courses");
                    if(courseResponse.IsSuccessStatusCode)
                    {
                        var courseContent = await courseResponse.Content.ReadAsStringAsync();
                        courses = JsonConvert.DeserializeObject<List<Course>>(courseContent);
                        if(courses != null)
                        {
                            foreach(var course in courses)
                            {
                                var assigmentResponse = await client.GetAsync($"{_canvasApiUrl}/courses/{course.Id}/assignments");
                                if(assigmentResponse.IsSuccessStatusCode)
                                {
                                    var assigmentContent = await assigmentResponse.Content.ReadAsStringAsync();
                                    assigments = JsonConvert.DeserializeObject<List<Assessment>>(assigmentContent);
                                    foreach (var assignment in assigments)
                                    {
                                        var greadResponse = await client.GetAsync($"{_canvasApiUrl}/courses/{course.Id}/assignments/{assignment.Id}/submissions/{student.Id}");
                                        if (greadResponse.IsSuccessStatusCode)
                                        {
                                            var greadContent = await greadResponse.Content.ReadAsStringAsync();
                                            var gread = JsonConvert.DeserializeObject<Grades>(greadContent);
                                            assignment.score = gread.score;
                                        }
                                    }
                                    course.Assesments = assigments;
                                }
                            }
                            student.Courses = courses;
                        }
                    }
                }
                return student;
            }
            else
            {
                string message = $"Error getting user {userId} from Canvas API: {response.StatusCode}";
                _logger.LogError(message);
                return null;
            }
        }
    }
}
