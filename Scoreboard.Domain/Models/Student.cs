
using Newtonsoft.Json;

namespace Scoreboard.Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StreamId { get; set; }
        public Stream Stream { get; set; }
        public List<Course>? Courses { get; set; }
        public List<StudentAssesment>? StudentAssesments { get; set; }
        public StudentTotalPoint? StudentTotalPoint { get; set; }
    }
}
