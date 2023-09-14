using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;
using Scoreboard.Service.Canvas;
using Scoreboard.Service.Canvas.Students;

namespace Scoreboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanvasController : ControllerBase
{
    private readonly CanvasAppServices _canvasAppServices;
    private readonly StudentAppServices _studentAppServices;
    private readonly IStudentRepository _studentRepository;
    public CanvasController(CanvasAppServices canvasAppServices, StudentAppServices studentAppServices, IStudentRepository studentRepository)
    {
        _canvasAppServices = canvasAppServices;
        _studentAppServices = studentAppServices;
        _studentRepository = studentRepository;
    }

    //[HttpGet("courses/{courseId}")]
    //public async Task<ActionResult<CanvasCourseResponseDto>> GetCourseAsync(int courseId)
    //{
    //    var course = await _canvasAppServices.GetCourseAsync(courseId);
    //    if (course == null)
    //    {
    //        return BadRequest($"Course {courseId} not found");
    //    }
    //    return Ok(course);
    //}

    //[HttpGet("users/{userId}/courses")]
    //public async Task<ActionResult<List<CanvasCourseResponseDto>>> GetCoursesForUserAsync(int userId)
    //{
    //    var courses = await _canvasAppServices.GetCoursesForUserAsync(userId);
    //    if (courses == null)
    //    {
    //        return BadRequest($"Courses for user {userId} not found");
    //    }
    //    return Ok(courses);
    //}

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

    [HttpDelete("student/{Id}")]
    public async Task<ActionResult<Student>> DeleteStudentAsync(int Id)
    {
        var student = await _studentRepository.DeleteStudentAsync(Id);
        if (student == null)
        {
            return BadRequest($"User {Id} not found");
        }
        return Ok(student);
    }
    [HttpGet("studentDetails{Id}")]
    public async Task<ActionResult<GetStudentDto>> GetStudentDetails(int Id)
    {
        var student = await _studentRepository.GetStudentDetails(Id);
        if (student == null)
        {
            return BadRequest($"User {Id} not found");
        }
        return Ok(student);
    }

    [HttpGet("studentsDetails")]
    public async Task<ActionResult<List<GetStudentDto>>> GetStudentsDetails()
    {
        var students = await _studentRepository.GetStudentsDetails();
        if (students == null)
        {
            return BadRequest($"Users not found");
        }
        return Ok(students);
    }
}
