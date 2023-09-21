using Scoreboard.Domain.Models;

namespace Scoreboard.Contracts.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StreamId { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assessment> Assessments { get; set; }
        public List<StudentAssessment> StudentAssessments { get; set; }
    }
}
