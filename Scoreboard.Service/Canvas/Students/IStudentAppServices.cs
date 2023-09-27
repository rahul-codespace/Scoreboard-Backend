using Scoreboard.Contracts.Students;

namespace Scoreboard.Service.Canvas.Students
{
    public interface IStudentAppServices
    {
        Task SeedStudentDataInDatabaseAsync(List<StudentDto> students);
        Task SeedData(CancellationToken stoppingToken);
        Task<Object> RegisterStudent(RegisterStudentDto input);
    }
}
