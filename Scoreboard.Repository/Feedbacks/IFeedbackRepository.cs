using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Feedbacks
{
    public interface IFeedbackRepository
    {
        Task<Feedback> CreateFeedbackAsync(Feedback feedback);
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<List<Feedback>> GetFeedbacksAsync();
        Task<List<Feedback>> GetFeedbacksByStudentIdAsync(int studentId);
    }
}