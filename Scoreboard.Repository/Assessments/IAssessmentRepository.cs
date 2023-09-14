using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Assessments
{
    public interface IAssessmentRepository
    {
        Task<List<Assessment>> AddAssessmentListAsync(List<Assessment> assessments);
    }
}
