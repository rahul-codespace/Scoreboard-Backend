using Newtonsoft.Json;

namespace Scoreboard.Domain.Models;

public class SubmissionComment
{
    public int Id { get; set; }
    public string? Comment { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int AssessmentId { get; set; }
    public StudentAssessment? StudentAssessment { get; set; }

    [JsonProperty("author_id")]
    public string AuthorId { get; set; }

    [JsonProperty("author_name")]
    public string AuthorName { get; set; }
}
