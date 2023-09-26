using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.StudentAssessments
{
    public class StudentAssessmentRepository : IStudentAssessmentRepository
    {
        private readonly ScoreboardDbContext _context;
        public StudentAssessmentRepository(ScoreboardDbContext context)
        {
            _context = context;
        }
        public async Task<StudentAssessment> AddStudentAssessmentAsync(StudentAssessment studentAssessments)
        {
            var assisment = _context.StudentAssessments.FirstOrDefault(a => studentAssessments.StudentId == a.StudentId && studentAssessments.AssessmentId == a.AssessmentId);
            if (assisment == null)
            {
                _context.StudentAssessments.Add(studentAssessments);
            }
            await _context.SaveChangesAsync();
            return studentAssessments;
        }

        public async Task<List<StudentAssessment>> AddListAsync(List<StudentAssessment> studentAssessments)
        {
            if (studentAssessments != null) {
                foreach (var assessment in studentAssessments)
                {
                    var existingAssessment = _context.StudentAssessments.FirstOrDefault(a => assessment.StudentId == a.StudentId && assessment.AssessmentId == a.AssessmentId);

                    if (existingAssessment == null)
                    {
                        _context.StudentAssessments.Add(assessment);
                    }
                }
                await _context.SaveChangesAsync();
            }
            return studentAssessments;
        }

        public async Task DeleteAllStudentAssissmentRecordAsync(int studentId)
        {
            var studentAssessments = _context.StudentAssessments.Where(a => a.StudentId == studentId);
            if (studentAssessments != null)
            {
                _context.StudentAssessments.RemoveRange(studentAssessments);
                await _context.SaveChangesAsync();
            }
        }
    }
}
