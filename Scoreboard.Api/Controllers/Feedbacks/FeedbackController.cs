using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Feedbacks;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Feedbacks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scoreboard.Repository.Auths;

namespace Scoreboard.Api.Controllers.Feedbacks
{
    [Route("api/[controller][Action]")]
    [ApiController]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAuthRepository _userRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository, IAuthRepository userRepository)
        {
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedbackAsync(CreateFeedbackDto feedback)
        {
            var user = await _userRepository.GetUser(User.FindFirstValue(ClaimTypes.Email)!);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var newFeedback = new Feedback
            {
                Reviewer = userRole,
                ReviewerName = user.Name,
                StudentId = feedback.StudentId,
                FeedBackPoints = feedback.FeedBackPoints,
                Rating = feedback.Rating
            };
            var createdFeedback = await _feedbackRepository.CreateFeedbackAsync(newFeedback);
            return Ok(createdFeedback);
        }
    }
}
