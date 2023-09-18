using Microsoft.AspNetCore.Mvc;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Courses;

namespace Scoreboard.Api.Controllers.Courses;

[ApiController]
[Route("api/[controller][Action]")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Course>>> GetCoursesAsync()
    {
        var courses = await _courseRepository.GetCoursesAsync();
        if (courses == null)
        {
            return BadRequest($"Courses not found");
        }
        return Ok(courses);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourseAsync(int id)
    {
        var course = await _courseRepository.GetCourseAsync(id);
        if (course == null)
        {
            return BadRequest($"Course {id} not found");
        }
        return Ok(course);
    }
    [HttpPost]
    public async Task<ActionResult<Course>> AddCourseAsync(Course course)
    {
        var result = await _courseRepository.AddCourseAsync(course);
        if (result == null)
        {
            return BadRequest($"Course {course.Id} not found");
        }
        return Ok(result);
    }
    [HttpPut]
    public async Task<ActionResult<Course>> UpdateCourseAsync(Course course)
    {
        var result = await _courseRepository.UpdateCourseAsync(course);
        if (result == null)
        {
            return BadRequest($"Course {course.Id} not found");
        }
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Course>> DeleteCourseAsync(int id)
    {
        var result = await _courseRepository.DeleteCourseAsync(id);
        if (result == null)
        {
            return BadRequest($"Course {id} not found");
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<List<Course>>> AddCourseListAsync(List<Course> courses)
    {
        var result = await _courseRepository.AddCourseListAsync(courses);
        if (result == null)
        {
            return BadRequest($"Courses not found");
        }
        return Ok(result);
    }
}
