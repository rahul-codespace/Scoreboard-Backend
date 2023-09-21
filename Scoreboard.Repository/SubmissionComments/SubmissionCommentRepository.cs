using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.SubmissionComments
{
    public class SubmissionCommentRepository : ISubmissionCommentRepository
    {
        private readonly ScoreboardDbContext _context;

        public SubmissionCommentRepository(ScoreboardDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubmissionComment>> AddListAsync(List<SubmissionComment> submissionComments)
        {
            if (submissionComments != null)
            {
                foreach (var sc in submissionComments)
                {
                    var existingAssessment = _context.SubmissionComments.FirstOrDefault(s => s.Id == sc.Id);

                    if (existingAssessment == null)
                    {
                        _context.SubmissionComments.Add(sc);
                    }
                }
                await _context.SaveChangesAsync();
            }
            return submissionComments;
        }
    }
}
