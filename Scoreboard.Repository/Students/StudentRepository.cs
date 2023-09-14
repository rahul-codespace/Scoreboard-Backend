﻿using Microsoft.EntityFrameworkCore;
using Scoreboard.Contracts.Students;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Students
{
    public class StudentRepository
    {
        private readonly ScoreboardDbContext _context;

        public StudentRepository(ScoreboardDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<GetStudentDto> GetStudentDetails(int id)
        {
            var student= await _context.Students.Include(s => s.Stream).Include(a => a.StudentAssesments).Include(a=> a.StudentTotalPoint).Select(s => new GetStudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Stream = s.Stream,
                StudentAssesments = s.StudentAssesments
            }).FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
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
}