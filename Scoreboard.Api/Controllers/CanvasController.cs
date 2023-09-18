using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;
using Scoreboard.Service.Canvas.Students;

namespace Scoreboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanvasController : ControllerBase
{
    private readonly StudentAppServices _studentAppServices;
    public CanvasController(StudentAppServices studentAppServices)
    {
        _studentAppServices = studentAppServices;
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<Student>> GetStudentAsync(int studentId)
    {
        var student = await _studentAppServices.SeedStudentDataAsync(studentId);
        if (student == null)
        {
            return BadRequest($"User {studentId} not found");
        }
        return Ok(student);
    }
}
