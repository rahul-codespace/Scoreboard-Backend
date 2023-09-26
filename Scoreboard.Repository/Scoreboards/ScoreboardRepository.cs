using Microsoft.EntityFrameworkCore;
using Scoreboard.Contracts.Scoreboards;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System.Collections;

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

        public async Task<StudentCourseAssignmentDto> GetStudentsInfo(int studentId)
        {
            var student = await _context.Students.Include(s => s.StudentAssessments).ThenInclude(sa => sa.Assessment).SingleOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return null;
            }

            var studentInfo = new Studentinfo
            {
                Id = student.Id,
                Name = student.Name,
                Courses = new List<CoursesDto>()
            };

            var courseIds = student.StudentAssessments.Select(sa => sa.Assessment.CourseId).Distinct();

            foreach (var courseId in courseIds)
            {
                var course = await _context.Courses.FindAsync(courseId);

                if (course == null) continue;

                var assignments = student.StudentAssessments
                    .Where(sa => sa.Assessment.CourseId == courseId)
                    .Select(sa => new AssignmentsDto
                    {
                        Id = sa.Assessment.Id,
                        Name = sa.Assessment.Name,
                        Point = sa.Assessment.Point,
                        AchievedPoint = (float?)(sa.AchievedPoints ?? float.NaN),
                        Percentage = (float?)(sa.AchievedPoints / sa.Assessment.Point * 100 ?? float.NaN),
                    })
                    .ToList();

                studentInfo.Courses.Add(new CoursesDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Assignments = assignments
                });
            }

            return new StudentCourseAssignmentDto
            {
                Studentinfo = studentInfo
            };
        }

        public async Task<StudentCourseAssignmentDto> GetStudentsInfoWithFeedback(int studentId)
        {
            var student = await _context.Students.Include(s => s.StudentAssessments).ThenInclude(sa => sa.Assessment).SingleOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return null;
            }

            var studentInfo = new Studentinfo
            {
                Id = student.Id,
                Name = student.Name,
                Courses = new List<CoursesDto>()
            };

            var courseIds = student.StudentAssessments.Select(sa => sa.Assessment.CourseId).Distinct();

            foreach (var courseId in courseIds)
            {
                var course = await _context.Courses.FindAsync(courseId);

                if (course == null) continue;

                var assignments = student.StudentAssessments
                    .Where(sa => sa.Assessment.CourseId == courseId)
                    .Select(sa => new AssignmentsDto
                    {
                        Id = sa.Assessment.Id,
                        Name = sa.Assessment.Name,
                        Point = sa.Assessment.Point,
                        AchievedPoint = (float?)(sa.AchievedPoints ?? float.NaN),
                        Percentage = (float?)(sa.AchievedPoints / sa.Assessment.Point * 100 ?? float.NaN),
                        Comments = _context.SubmissionComments
                            .Where(sc => sc.AssessmentId == sa.Assessment.Id && sc.StudentId == studentId)
                            .Select(sc => new CommentsDto
                            {
                                Id = sc.Id,
                                Comment = sc.Comment,
                                AuthorId = sc.AuthorId,
                                AuthorName = sc.AuthorName
                            })
                            .ToList()
                    })
                    .ToList();

                studentInfo.Courses.Add(new CoursesDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Assignments = assignments
                });
            }

            return new StudentCourseAssignmentDto
            {
                Studentinfo = studentInfo
            };
        }
    }
}