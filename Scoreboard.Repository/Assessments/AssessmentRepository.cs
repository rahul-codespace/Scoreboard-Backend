using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Assessments
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly ScoreboardDbContext _context;
        public AssessmentRepository(ScoreboardDbContext context)
        {
            _context = context;
        }
        public async Task<List<Assessment>> AddAssessmentListAsync(List<Assessment> assessments)
        {
            foreach (var item in assessments)
            {
               var assissment = _context.Assessments.FirstOrDefault(a => a.Id == item.Id);
                if (assissment == null)
                {
                    _context.Assessments.Add(item);
                }
            }
            await _context.SaveChangesAsync();
            return assessments;
        }
    }
}
