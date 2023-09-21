using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.StreamCourses
{
    public interface IStreamCoursesRepository
    {
        Task AddListAsync(int streamId, List<Course> courses);
        Task<StreamCourse> GetStreamCourseAsync(int streamId, int courseId);
        Task<List<StreamCourse>> GetStreamCoursesAsync();
    }
}
