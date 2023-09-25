using Microsoft.EntityFrameworkCore;
using Scoreboard.Contracts.Scoreboards;
using Scoreboard.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Scoreboards
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private readonly ScoreboardDbContext _context;

        public ScoreboardRepository(ScoreboardDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentInfoDto>> GetStudentsInfo()
        {
            var studentInfo = await (from student in _context.Students
                                     join stream in _context.Streams on student.StreamId equals stream.Id into streamGroup
                                     from stream in streamGroup.DefaultIfEmpty()
                                     join totalPoint in _context.StudentTotalPoints on student.Id equals totalPoint.StudentId into totalPointGroup
                                     from totalPoint in totalPointGroup.DefaultIfEmpty()
                                     select new StudentInfoDto
                                     {
                                         Id = student.Id,
                                         Name = student.Name,
                                         Stream = stream,
                                         Percentage = totalPoint.PercentageScore ?? double.NaN,
                                     })
                                     .OrderByDescending(s => double.IsNaN(s.Percentage) ? double.MinValue : s.Percentage)
                                     .ToListAsync();

            // Assign ranks based on percentage
            for (int i = 0; i < studentInfo.Count; i++)
            {
                studentInfo[i].Rank = i + 1;
            }

            return studentInfo;
        }


    }
}
