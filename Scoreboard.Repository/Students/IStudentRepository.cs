using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Students
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentAsync(int id);
        Task<GetStudentDto> GetStudentDetails(int id);
        Task<List<GetStudentDto>> GetStudentsDetails();
        Task<Student> AddStudentAsync(Student student);
        Task<Student?> DeleteStudentAsync(int id);
        Task<Student?> UpdateStudentAsync(Student student);
        Task<List<StudentDto>> GetStudentsAsync();
        Task RemoveTableDataAsync();
    }
}
