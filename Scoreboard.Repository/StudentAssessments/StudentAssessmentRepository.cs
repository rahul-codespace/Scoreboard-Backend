using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var assisment = _context.StudentAssesments.FirstOrDefault(a => studentAssessments.StudentId == a.StudentId && studentAssessments.AssessmentId == a.AssessmentId);
            if (assisment == null)
            {
                _context.StudentAssesments.Add(studentAssessments);
            }
            await _context.SaveChangesAsync();
            return studentAssessments;
        }

        public async Task<List<StudentAssessment>> AddStudentAssessmentsAsync(List<StudentAssessment> studentAssessments)
        {
            if (studentAssessments != null) {
                foreach (var assessment in studentAssessments)
                {
                    var existingAssessment = _context.StudentAssesments.FirstOrDefault(a => assessment.StudentId == a.StudentId && assessment.AssessmentId == a.AssessmentId);

                    if (existingAssessment == null)
                    {
                        _context.StudentAssesments.Add(assessment);
                    }
                }
                await _context.SaveChangesAsync();
            }
            return studentAssessments;
        }

        public async Task DeleteAllStudentAssissmentRecordAsync(int studentId)
        {
            var studentAssessments = _context.StudentAssesments.Where(a => a.StudentId == studentId);
            if (studentAssessments != null)
            {
                _context.StudentAssesments.RemoveRange(studentAssessments);
                await _context.SaveChangesAsync();
            }
        }
    }
}
