namespace Scoreboard.Domain.Models;

public class StreamCourse
{
    public int Id { get; set; }
    public int StreamId { get; set; }
    public int CourseId { get; set; }
    public Stream? Stream { get; set; }
    public Course? Course { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
