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
            var StudentAssissments = await _context.StudentAssesments.Where(a => a.StudentId == studentId && a.AchievedPoints != null).ToListAsync();
            var totalAchievedPoints = 0.0f;
            foreach (var assissment in StudentAssissments)
            {
                if (assissment.AchievedPoints != null)
                {
                    var Assignments = await _context.Assessments.FirstOrDefaultAsync(a => a.Id == assissment.AssessmentId);
                    totalAchievedPoints += Assignments.Point.Value;
                }
            }
            var totalPoints = StudentAssissments.Sum(a => a.AchievedPoints);
            var percentageScore = (totalPoints / totalAchievedPoints) * 100;


            var studentTotalPointNew = new StudentTotalPoint
            {
                StudentId = studentId,
                TotalPoints = totalAchievedPoints,
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
