using Newtonsoft.Json;
using Scoreboard.Domain.Models;

namespace Scoreboard.Contracts.Canvas.ResponseDto
{
    public class StudentAssessmentApiResponseDto
    {
        public int? Id { get; set; }
        public float? Score { get; set; }

        [JsonProperty("submission_comments")]
        public List<SubmissionComment> SubmissionComments { get; set; }
    }
}
