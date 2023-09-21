using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Courses
{
    public interface ICourseRepository
    {
        Task<Course?> GetCourseAsync(int id);
        Task<List<Course>> GetCoursesAsync();
        Task<Course> AddCourseAsync(Course course);
        Task<List<Course>> AddListAsync(List<Course> courses);
        Task<Course> UpdateCourseAsync(Course course);
        Task<Course?> DeleteCourseAsync(int id);
    }
}
