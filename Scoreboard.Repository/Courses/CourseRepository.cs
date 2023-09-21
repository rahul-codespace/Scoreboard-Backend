using Microsoft.EntityFrameworkCore;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Courses;

public class CourseRepository : ICourseRepository
{
    private readonly ScoreboardDbContext _context;

    public CourseRepository(ScoreboardDbContext context)
    {
        _context = context;
    }

    public async Task<Course?> GetCourseAsync(int id)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }
    public async Task<Course> AddCourseAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }
    public async Task<List<Course>> AddListAsync(List<Course> courses)
    {
        if (courses != null)
        {
            foreach (var item in courses)
            {
                var course = _context.Courses.FirstOrDefault(c => c.Id == item.Id);
                if (course == null)
                {
                    _context.Courses.Add(item);
                }
            }
            await _context.SaveChangesAsync();
        }
        return courses;
    }
    public async Task<Course> UpdateCourseAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }
    public async Task<Course?> DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        return course;
    }
}
