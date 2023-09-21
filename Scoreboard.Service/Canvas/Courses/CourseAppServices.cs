using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Courses;
using Scoreboard.Repository.Students;
using Scoreboard.Service.Canvas.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Service.Canvas.Courses
{
    public class CourseAppServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CourseAppServices> _logger;
        private readonly string _canvasApiUrl;
        private readonly string _canvasApiToken;
        private readonly ICourseRepository _courseRepository;

        public CourseAppServices(
            IConfiguration configuration, 
            ILogger<CourseAppServices> logger, 
            ICourseRepository courseRepository
            ){
            _logger = logger;
            _configuration = configuration;
            _canvasApiUrl = _configuration["Canvas:ApiUrl"]!;
            _canvasApiToken = _configuration["Canvas:ApiKey"]!;
            _courseRepository = courseRepository;
        }

        //public async Task<Course> CreateCourseAsync(int userId)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
        //    var response = await client.GetAsync($"{_canvasApiUrl}/users/{userId}/courses");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var course = JsonConvert.DeserializeObject<List<Course>>(content);
                
        //        if (course != null)
        //        {
        //            var existingStudent = await _courseRepository.GetCourseAsync(course.Id);
        //            if (existingStudent == null)
        //            {
        //                await _courseRepository.AddCourseAsync(course);
        //            }
        //            else
        //            {
        //               await _courseRepository.UpdateCourseAsync(course);
        //            }
        //        }
        //        return course;
        //    }
        //    else
        //    {
        //        string message = $"Error getting user {userId} from Canvas API: {response.StatusCode}";
        //        _logger.LogError(message);
        //        return null;
        //    }
        //}
    }
}
