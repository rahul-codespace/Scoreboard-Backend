namespace Scoreboard.Domain.Models;

public class Student
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public int StreamId { get; set; }
    public Stream? Stream { get; set; }
    public List<StudentAssessment>? StudentAssessments { get; set; }
    public StudentTotalPoint? StudentTotalPoint { get; set; }
    public List<Feedback> Feedbacks { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
