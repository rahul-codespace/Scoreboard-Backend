namespace Scoreboard.Domain.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string ReviewerName { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string FeedbackPoints { get; set; }
        public FeedbackType FeedbackType { get; set; }
        public int Rating { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool InCooperated { get; set; }
    }

    public enum FeedbackType
    {
        Negative,
        Neutral,
        Positive
    }
}
