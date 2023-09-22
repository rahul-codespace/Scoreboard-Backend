using Microsoft.EntityFrameworkCore;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.StreamCourses
{
    public class StreamCoursesRepository : IStreamCoursesRepository
    {
        public ScoreboardDbContext _context;

        public StreamCoursesRepository(ScoreboardDbContext context)
        {
            _context = context;
        }

        public async Task AddListAsync(int streamId, List<Course> courses)
        {
           foreach(var course in courses)
            {

                var stream = await _context.StreamCourses.FirstOrDefaultAsync(x => x.CourseId == course.Id && x.StreamId == streamId);
                if (stream == null)
                {
                     _context.StreamCourses.Add(new StreamCourse { CourseId = course.Id, StreamId = streamId });
                     await _context.SaveChangesAsync();
                }
                
            }
        }

        public async Task<StreamCourse> GetStreamCourseAsync(int streamId, int courseId)
        {
            return await _context.StreamCourses.Include(s=>s.Stream).Include(c=>c.Course).FirstOrDefaultAsync(x => x.CourseId == courseId && x.StreamId == streamId);
        }
        public async Task<List<StreamCourse>> GetStreamCoursesAsync()
        {
            var result = await (
                from sc in _context.StreamCourses
                join s in _context.Streams on sc.StreamId equals s.Id into streamGroup
                from stream in streamGroup.DefaultIfEmpty()
                join c in _context.Courses on sc.CourseId equals c.Id into courseGroup
                from course in courseGroup.DefaultIfEmpty()
                select new StreamCourse
                {
                    StreamId = sc.StreamId,
                    CourseId = sc.CourseId,
                    CreatedAt = sc.CreatedAt,
                    Stream = stream,
                    Course = course,
                }
            ).ToListAsync();

            return result;
        }
    }
}
