using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Courses
{
    /// <summary>
    /// Represents a repository for managing Course objects.
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// Asynchronously gets a Course object by ID.
        /// </summary>
        /// <param name="id">The ID of the Course to retrieve.</param>
        /// <returns>A Task object representing the asynchronous operation with a nullable Course object.</returns>
        Task<Course?> GetCourseAsync(int id);

        /// <summary>
        /// Asynchronously gets a list of Course objects.
        /// </summary>
        /// <returns>A Task object representing the asynchronous operation with a List of Course objects.</returns>
        Task<List<Course>> GetCoursesAsync();

        /// <summary>
        /// Asynchronously adds a Course object.
        /// </summary>
        /// <param name="course">The Course object to add to the repository.</param>
        /// <returns>A Task object representing the asynchronous operation with the added Course object.</returns>
        Task<Course> AddCourseAsync(Course course);

        /// <summary>
        /// Asynchronously adds a list of Course objects.
        /// </summary>
        /// <param name="courses">The List of Courses to add to the repository.</param>
        /// <returns>A Task object representing the asynchronous operation with a List of added Course objects.</returns>
        Task<List<Course>> AddListAsync(List<Course> courses);

        /// <summary>
        /// Asynchronously updates a Course object.
        /// </summary>
        /// <param name="course">The Course object to update in the repository.</param>
        /// <returns>A Task object representing the asynchronous operation with the updated Course object.</returns>
        Task<Course> UpdateCourseAsync(Course course);

        /// <summary>
        /// Asynchronously deletes a Course object by ID.
        /// </summary>
        /// <param name="id">The ID of the Course to delete.</param>
        /// <returns>A Task object representing the asynchronous operation with a nullable Course object.</returns>
        Task<Course?> DeleteCourseAsync(int id);
    }
}
