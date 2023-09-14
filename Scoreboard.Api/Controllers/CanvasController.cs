using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Canvas.Dtos;
using Scoreboard.Domain.Models;
using Scoreboard.Service.Canvas;
using Scoreboard.Service.Canvas.Students;

namespace Scoreboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanvasController : ControllerBase
{
    private readonly CanvasAppServices _canvasAppServices;
    private readonly StudentAppServices _studentAppServices;
    public CanvasController(CanvasAppServices canvasAppServices, StudentAppServices studentAppServices)
    {
        _canvasAppServices = canvasAppServices;
        _studentAppServices = studentAppServices;
    }

    [HttpGet("courses/{courseId}")]
    public async Task<ActionResult<CanvasCourseResponseDto>> GetCourseAsync(int courseId)
    {
        var course = await _canvasAppServices.GetCourseAsync(courseId);
        if (course == null)
        {
            return BadRequest($"Course {courseId} not found");
        }
        return Ok(course);
    }

    [HttpGet("users/{userId}/courses")]
    public async Task<ActionResult<List<CanvasCourseResponseDto>>> GetCoursesForUserAsync(int userId)
    {
        var courses = await _canvasAppServices.GetCoursesForUserAsync(userId);
        if (courses == null)
        {
            return BadRequest($"Courses for user {userId} not found");
        }
        return Ok(courses);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<Student>> GetStudentAsync(int studentId)
    {
        var student = await _studentAppServices.CreateStudentAsync(studentId);
        if (student == null)
        {
            return BadRequest($"User {studentId} not found");
        }
        return Ok(student);
    }
}
