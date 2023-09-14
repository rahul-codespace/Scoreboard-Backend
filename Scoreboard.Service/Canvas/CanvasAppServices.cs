using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Scoreboard.Service.Canvas;

public class CanvasAppServices
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CanvasAppServices> _logger;
    private readonly string _canvasApiUrl;
    private readonly string _canvasApiToken;

    public CanvasAppServices(IConfiguration configuration, ILogger<CanvasAppServices> logger)
    {
        _logger = logger;
        _configuration = configuration;
        _canvasApiUrl = _configuration["Canvas:ApiUrl"];
        _canvasApiToken = _configuration["Canvas:ApiKey"];
    }

    //public async Task<CanvasCourseResponseDto> GetCourseAsync(int courseId)
    //{
    //    var client = new HttpClient();
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
    //    var response = await client.GetAsync($"{_canvasApiUrl}/courses/{courseId}");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var content = await response.Content.ReadAsStringAsync();
    //        var course = JsonConvert.DeserializeObject<CanvasCourseResponseDto>(content);
    //        return course;
    //    }
    //    else
    //    {
    //        string message = $"Error getting course {courseId} from Canvas API: {response.StatusCode}";
    //        _logger.LogError(message);
    //        return null;
    //    }
    //}

    //public async Task<List<CanvasCourseResponseDto>> GetCoursesForUserAsync(int userId)
    //{
    //    var client = new HttpClient();
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _canvasApiToken);
    //    var response = await client.GetAsync($"{_canvasApiUrl}/users/{userId}/courses");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var content = await response.Content.ReadAsStringAsync();
    //        var courses = JsonConvert.DeserializeObject<List<CanvasCourseResponseDto>>(content);
    //        return courses;
    //    }
    //    else
    //    {
    //        string message = $"Error getting courses for user {userId} from Canvas API: {response.StatusCode}";
    //        _logger.LogError(message);
    //        return null;
    //    }
    //}
}