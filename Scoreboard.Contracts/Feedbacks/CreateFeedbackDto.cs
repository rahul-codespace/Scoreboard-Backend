using System.ComponentModel.DataAnnotations;

namespace Scoreboard.Contracts.Feedbacks
{
    public class CreateFeedbackDto
    {

        [Required(ErrorMessage = "Student's Id is required.")]
        public required int StudentId { get; set; }
        [Required(ErrorMessage = "Feedback points can not be null.")]
        public string FeedBackPoints { get; set; }

        [Range(1, 10, ErrorMessage = "Please rate between 1 and 10.")]
        public int Rating { get; set; }
    }
}
