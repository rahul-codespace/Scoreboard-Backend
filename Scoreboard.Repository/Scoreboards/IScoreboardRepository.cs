using Scoreboard.Contracts.Scoreboards;
using System.Collections;

namespace Scoreboard.Repository.Scoreboards
{
    public interface IScoreboardRepository
    {
        Task<List<StudentInfoDto>> GetStudentsInfo();
        Task<StudentCourseAssignmentDto> GetStudentsInfo(int studentId);
        Task<StudentCourseAssignmentDto> GetStudentsInfoWithFeedback(int studentId);
    }
}