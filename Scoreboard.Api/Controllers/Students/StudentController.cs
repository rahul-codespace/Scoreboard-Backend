using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Students;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;

namespace Scoreboard.Api.Controllers.Students
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentAsync(int id)
        {
            var student = await _studentRepository.GetStudentAsync(id);
            if (student == null)
            {
                return BadRequest($"Student {id} not found");
            }
            return Ok(student);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<GetStudentDto>> GetStudentDetails(int id)
        {
            var student = await _studentRepository.GetStudentDetails(id);
            if (student == null)
            {
                return BadRequest($"Student {id} not found");
            }
            return Ok(student);
        }

    }
}
