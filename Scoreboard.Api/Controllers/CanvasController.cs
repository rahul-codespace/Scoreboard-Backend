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

    [HttpGet]
    public async Task<ActionResult> SeedStudentsDataAsync()
    {
        try
        {
            var students = await _studentRepository.GetStudentsAsync();
            var studentsData = await _getStudentDataServices.GetStudentsDataFromCanvas(students);
            await _studentAppServices.SeedStudentDataInDatabaseAsync(studentsData);
            return Ok("Data saved successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
