using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Feedbacks;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Feedbacks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Scoreboard.Repository.Auths;
using Scoreboard.Service.Email;
using Scoreboard.Contracts.Emails;

namespace Scoreboard.Api.Controllers.Feedbacks
{
    [Route("api/[controller][Action]")]
    [ApiController]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAuthRepository _userRepository;
        private readonly IEmailServices _emailServices;

        public FeedbackController(IFeedbackRepository feedbackRepository, IAuthRepository userRepository, IEmailServices emailServices)
        {
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
            _emailServices = emailServices;
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
            _emailServices.SendEmail(new MessageDto(new List<string> { $"{user.Email}" }, "New Feedback", $"New Feedback from {user.Name}: \n {feedback.FeedBackPoints}"));
            return Ok(createdFeedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacksAsync()
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksAsync();
            return Ok(feedbacks);
        }
        [HttpGet]
        public async Task<IActionResult> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
            return Ok(feedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacksByStudentIdAsync(int studentId)
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksByStudentIdAsync(studentId);
            return Ok(feedbacks);
        }
    }
}
