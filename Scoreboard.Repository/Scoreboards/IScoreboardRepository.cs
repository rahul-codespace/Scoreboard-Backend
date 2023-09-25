using Scoreboard.Contracts.Scoreboards;

namespace Scoreboard.Repository.Scoreboards
{
    public interface IScoreboardRepository
    {
        Task<List<StudentInfoDto>> GetStudentsInfo();
    }
}