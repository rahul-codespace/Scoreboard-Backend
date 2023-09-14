

using Scoreboard.Domain.Models;

namespace Scoreboard.Contracts.Students
{
    public class GetStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Domain.Models.Stream Stream { get; set; }
        public List<Course> courses { get; set; }
        public List<StudentAssessment>? StudentAssessments { get; set; }
    }
}
