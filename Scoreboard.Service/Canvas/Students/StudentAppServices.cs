

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

        //public async Task<Student> CreateStudentAsync(int userId)
        //{
        //    // Initialize the student with a default StreamId.
        //    var student = new Student();

        //    // Fetch user information asynchronously.
        //    var userResponse = await GetCanvasApiResponseAsync($"users/{userId}");

        //    if (userResponse == null || !userResponse.IsSuccessStatusCode)
        //    {
        //        string message = $"Error getting user {userId} from Canvas API: {(userResponse != null ? userResponse.StatusCode.ToString() : "Unknown")}";
        //        _logger.LogError(message);
        //        return null;
        //    }

        //    var userContent = await userResponse.Content.ReadAsStringAsync();
        //    student = JsonConvert.DeserializeObject<Student>(userContent);

        //    if (student != null)
        //    {
        //        student.StreamId = 1;
        //        // Fetch courses asynchronously.
        //        student.Courses = await GetCoursesAsync(student.Id);

        //        foreach (var course in student.Courses)
        //        {
        //            // Fetch assignments asynchronously.
        //            course.Assessments = await GetAssignmentsAsync(course.Id, student.Id);

        //            // Fetch grades for each assignment asynchronously.
        //            foreach (var assignment in course.Assessments)
        //            {
        //                var grade = await GetAssignmentGradeAsync(course.Id, assignment.Id, student.Id);
        //                assignment.Score = grade;
        //            }
        //        }
        //    }

        //    return student;
        //}

        //private async Task<HttpResponseMessage> GetCanvasApiResponseAsync(string endpoint)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
        //    return await client.GetAsync($"{_canvasApiUrl}/{endpoint}");
        //}

        //private async Task<List<Course>> GetCoursesAsync(int userId)
        //{
        //    var courseResponse = await GetCanvasApiResponseAsync($"users/{userId}/courses");

        //    if (courseResponse != null && courseResponse.IsSuccessStatusCode)
        //    {
        //        var courseContent = await courseResponse.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<List<Course>>(courseContent);
        //    }

        //    return new List<Course>();
        //}

        //private async Task<List<Assessment>> GetAssignmentsAsync(int courseId, int studentId)
        //{
        //    var assignmentResponse = await GetCanvasApiResponseAsync($"courses/{courseId}/assignments");

        //    if (assignmentResponse != null && assignmentResponse.IsSuccessStatusCode)
        //    {
        //        var assignmentContent = await assignmentResponse.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<List<Assessment>>(assignmentContent);
        //    }

        //    return new List<Assessment>();
        //}

        //private async Task<float?> GetAssignmentGradeAsync(int courseId, int assignmentId, int studentId)
        //{
        //    var gradeResponse = await GetCanvasApiResponseAsync($"courses/{courseId}/assignments/{assignmentId}/submissions/{studentId}");

        //    if (gradeResponse != null && gradeResponse.IsSuccessStatusCode)
        //    {
        //        var gradeContent = await gradeResponse.Content.ReadAsStringAsync();
        //        var grade = JsonConvert.DeserializeObject<Grades>(gradeContent);
        //        return grade.score;
        //    }
        //    return null;
        //}

    }
}
