
using Newtonsoft.Json;

namespace Scoreboard.Domain.Models
{
    public class Student
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int StreamId { get; set; }
        public Stream Stream { get; set; }
        public List<Course>? Courses { get; set; }
        public List<StudentAssesment>? StudentAssesments { get; set; }
        public StudentTotalPoint? StudentTotalPoint { get; set; }
    }
}
