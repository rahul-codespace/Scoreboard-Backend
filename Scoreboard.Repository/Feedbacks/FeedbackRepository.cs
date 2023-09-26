using Microsoft.EntityFrameworkCore;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Feedbacks
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ScoreboardDbContext _context;

        public FeedbackRepository(ScoreboardDbContext context)
        {
            _context = context;
        }

        public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<List<Feedback>> GetFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }
        public async Task<List<Feedback>> GetFeedbacksByStudentIdAsync(int studentId)
        {
            return await _context.Feedbacks.Where(f => f.StudentId == studentId).ToListAsync();
        }
    }
}
