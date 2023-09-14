using Microsoft.EntityFrameworkCore;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.StudentTotalPoints
{
    public class StudentTotalPointRepository : IStudentTotalPointRepository
    {
        private readonly ScoreboardDbContext _context;

        public StudentTotalPointRepository(ScoreboardDbContext context)
        {
            _context = context;
        }

        public async Task<StudentTotalPoint> AddStudentTotalPointAsync(int studentId)
        {
            var allAssisments = await _context.StudentAssesments.Where(a => a.StudentId == studentId && a.AchievedPoints != null).ToListAsync();
            var allPoints = 0.0f;
            foreach (var assissment in allAssisments)
            {
                allPoints += assissment.Assessment.Point ?? 0.0f;
            }
            var totalPoints = allAssisments.Sum(a => a.AchievedPoints);
            var percentageScore = (totalPoints / allPoints) * 100;
            var studentTotalPointNew = new StudentTotalPoint
            {
                StudentId = studentId,
                TotalPoints = allPoints,
                TotalAchievedPoints = totalPoints ?? 0.0f,
                PercentageScore = percentageScore ?? 0.0f
            };
            var studentTotalPoint = await _context.StudentTotalPoints.FirstOrDefaultAsync(s => s.StudentId == studentId);
            if (studentTotalPoint == null)
            {
                _context.StudentTotalPoints.Add(studentTotalPointNew);
                await _context.SaveChangesAsync();
                return studentTotalPointNew;
            }
            _context.StudentTotalPoints.Update(studentTotalPointNew);
            await _context.SaveChangesAsync();
            return studentTotalPointNew;
        }
    }
}
