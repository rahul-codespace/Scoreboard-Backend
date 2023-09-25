using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Students;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Students;
using Scoreboard.Service.Canvas.Students;

namespace Scoreboard.Api.Controllers.Students
{
    [ApiController]
    [Route("api/[controller][Action]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentAppServices _studentAppServices;

        public StudentController(IStudentRepository studentRepository, IStudentAppServices studentAppServices)
        {
            _studentRepository = studentRepository;
            _studentAppServices = studentAppServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetStudentDto>>> GetStudentsAsync()
        {
            var students = await _studentRepository.GetStudentsDetails();
            if (students == null)
            {
                return BadRequest($"Students not found");
            }
            return Ok(students);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentDto>> GetStudentAsync(int id)
        {
            var student = await _studentRepository.GetStudentDetails(id);
            if (student == null)
            {
                return BadRequest($"Student {id} not found");
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> RegisterStudent(RegisterStudentDto input)
        {
            var result = await _studentAppServices.RegisterStudent(input);
            if (result == null)
            {
                return BadRequest($"Student {input.Id} not found");
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudentAsync(Student student)
        {
            var result = await _studentRepository.UpdateStudentAsync(student);
            if (result == null)
            {
                return BadRequest($"Student {student.Id} not found");
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudentAsync(int id)
        {
            var result = await _studentRepository.DeleteStudentAsync(id);
            if (result == null)
            {
                return BadRequest($"Student {id} not found");
            }
            return Ok(result);
        }
    }
}
