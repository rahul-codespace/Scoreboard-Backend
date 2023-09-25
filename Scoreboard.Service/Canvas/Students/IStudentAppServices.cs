using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Service.Canvas.Students
{
    public interface IStudentAppServices
    {
        Task SeedStudentDataInDatabaseAsync(List<StudentDto> students);
        Task SeedData(CancellationToken stoppingToken);
        Task<Student> RegisterStudent(RegisterStudentDto input);
    }
}
