using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.StudentAssessments
{
    public interface IStudentAssessmentRepository
    {
        Task<StudentAssessment> AddStudentAssessmentAsync(StudentAssessment studentAssessments);
        Task DeleteAllStudentAssissmentRecordAsync(int studentId);
        Task<List<StudentAssessment>> AddStudentAssessmentsAsync(List<StudentAssessment> studentAssessments);
    }
}
