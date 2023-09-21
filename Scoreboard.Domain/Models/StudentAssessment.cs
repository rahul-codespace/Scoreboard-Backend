namespace Scoreboard.Domain.Models;

public class StudentAssessment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int AssessmentId { get; set; }
    public Assessment? Assessment { get; set; }
    public float? AchievedPoints { get; set; }
    public List<SubmissionComment>? SubmissionComments { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
