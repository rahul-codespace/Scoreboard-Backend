using Microsoft.EntityFrameworkCore;
using Scoreboard.Contracts.Students;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;

namespace Scoreboard.Repository.Students;

public class StudentRepository : IStudentRepository
{
    private readonly ScoreboardDbContext _context;

    public StudentRepository(ScoreboardDbContext context)
    {
        _context = context;
    }

    public async Task RemoveTableDataAsync()
    {
        _context.Courses.RemoveRange(_context.Courses);
        _context.StudentTotalPoints.RemoveRange(_context.StudentTotalPoints);
        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentAsync(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<GetStudentDto> GetStudentDetails(int id)
    {
        var student= await _context.Students.Include(s => s.Stream).Include(a => a.StudentAssessments).Include(a=> a.StudentTotalPoint).Select(s => new GetStudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Stream = s.Stream,
            StudentAssessments = s.StudentAssessments,
            StudentTotalPoint = s.StudentTotalPoint
        }).FirstOrDefaultAsync(s => s.Id == id);
        return student;
    }
    public async Task<List<GetStudentDto>> GetStudentsDetails()
    {
        var students = await _context.Students.Include(s => s.Stream).Include(a => a.StudentAssessments).Include(a => a.StudentTotalPoint).Select(s => new GetStudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Stream = s.Stream,
            StudentAssessments = s.StudentAssessments,
            StudentTotalPoint = s.StudentTotalPoint
        }).ToListAsync();
        return students;
    }
    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }
    public async Task<List<int>> GetStudentsIds()
    {
        return await _context.Students.Select(s => s.Id).ToListAsync();
    }
    public async Task<Student> AddStudentAsync(Student student)
    {
        var result = _context.Students.FirstOrDefault(s => s.Id == student.Id);
        if (result == null)
        {
            _context.Students.Add(student);
        }
        await _context.SaveChangesAsync();
        return student;

    }
    public async Task<Student> UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return student;
    }
    public async Task<Student?> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return student;
    }
}
