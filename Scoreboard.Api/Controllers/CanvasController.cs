using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;
using Scoreboard.Service.Canvas;
using Scoreboard.Service.Canvas.Students;

namespace Scoreboard.Api.Controllers;

[ApiController]
[Route("api/[controller][Action]")]
public class CanvasController : ControllerBase
{
    private readonly IStudentAppServices _studentAppServices;
    private readonly IGetStudentDataServices _getStudentDataServices;
    private readonly IStudentRepository _studentRepository;
    public CanvasController(StudentAppServices studentAppServices, IStudentRepository studentRepository, IGetStudentDataServices getStudentDataServices)
    {
        _getStudentDataServices = getStudentDataServices;
        _studentAppServices = studentAppServices;
        _studentRepository = studentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> SeedStudentsDataAsync()
    {
        var studentIds = await _studentRepository.GetStudentsIds();
        var students = await _getStudentDataServices.SeedStudentsDataAsync(studentIds);
        var result = await _studentAppServices.SeedStudentDataInDatabaseAsync(students);
        if (result == null)
        {
            return BadRequest($"Students not found");
        }
        return Ok(students);
    }
}
