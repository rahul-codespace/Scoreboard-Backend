using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Canvas.Dtos;
using Scoreboard.Service.Canvas;

namespace Scoreboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanvasController : ControllerBase
{
    private readonly CanvasAppServices _canvasAppServices;
    public CanvasController(CanvasAppServices canvasAppServices)
    {
        _canvasAppServices = canvasAppServices;
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

}
