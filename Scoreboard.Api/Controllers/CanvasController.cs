using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;
using Scoreboard.Service.Canvas.Students;

namespace Scoreboard.Api.Controllers;

[ApiController]
[Route("api/[controller][Action]")]
public class CanvasController : ControllerBase
{
    private readonly StudentAppServices _studentAppServices;
    private readonly IStudentRepository _studentRepository;
    public CanvasController(StudentAppServices studentAppServices, IStudentRepository studentRepository)
    {
        _studentAppServices = studentAppServices;
        _studentRepository = studentRepository;
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult<Student>> SeedStudentDataAsync(int studentId)
    {
        var student = await _studentAppServices.SeedStudentDataAsync(studentId);
        if (student == null)
        {
            return BadRequest($"User {studentId} not found");
        }
        return Ok(student);
    }
    [HttpPost]
    public async Task<ActionResult<Student>> SeedStudentsDataAsync()
    {
        var studentIds = await _studentRepository.GetStudentsIds();
        var students = await _studentAppServices.SeedStudentsDataAsync(studentIds);
        var result = await _studentAppServices.SeedStudentDataInDatabaseAsync(students);
        if (result == null)
        {
            return BadRequest($"Students not found");
        }
        return Ok(result);
    }
}
