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
    public CanvasController(IStudentAppServices studentAppServices, IStudentRepository studentRepository, IGetStudentDataServices getStudentDataServices)
    {
        _getStudentDataServices = getStudentDataServices;
        _studentAppServices = studentAppServices;
        _studentRepository = studentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> SeedStudentsDataAsync()
    {
        var students = await _studentRepository.GetStudentsAsync();
        var studentsData = await _getStudentDataServices.GetStudentsDataFromCanvas(students);
        var result = await _studentAppServices.SeedStudentDataInDatabaseAsync(studentsData);
        if (result == null)
        {
            return BadRequest($"Students not found");
        }
        return Ok(students);
    }
}
