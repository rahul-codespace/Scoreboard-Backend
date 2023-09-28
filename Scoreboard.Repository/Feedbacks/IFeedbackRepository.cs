using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Feedbacks
{
    /// <summary>
    /// Interface defining methods to interact with feedback objects stored in the database
    /// </summary>
    public interface IFeedbackRepository
    {
        /// <summary>
        /// Creates a new feedback object in the database
        /// </summary>
        /// <param name="feedback">The feedback object to create</param>
        /// <returns>The newly created feedback object</returns>
        Task<Feedback> CreateFeedbackAsync(Feedback feedback);

        /// <summary>
        /// Retrieves a specific feedback object from the database by its ID
        /// </summary>
        /// <param name="id">The ID of the feedback object to retrieve</param>
        /// <returns>The feedback object with the specified ID</returns>
        Task<Feedback> GetFeedbackByIdAsync(int id);

        /// <summary>
        /// Retrieves a list of all feedback objects stored in the database
        /// </summary>
        /// <returns>A list of all feedback objects in the database</returns>
        Task<List<Feedback>> GetFeedbacksAsync();

        /// <summary>
        /// Retrieves a list of all feedback objects associated with a specific Student ID
        /// </summary>
        /// <param name="studentId">The ID of the Student whose feedback to retrieve</param>
        /// <returns>A list of all feedback objects associated with the specified Student ID</returns>
        Task<List<Feedback>> GetFeedbacksByStudentIdAsync(int studentId);
    }
}