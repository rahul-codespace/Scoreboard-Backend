using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Assessments
{
    public interface IAssessmentRepository
    {
        Task<List<Assessment>> AddListAsync(List<Assessment> assessments);
    }
}
