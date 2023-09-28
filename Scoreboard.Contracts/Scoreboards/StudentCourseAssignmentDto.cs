using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Scoreboards
{
    public class StudentCourseAssignmentDto
    {
        public Studentinfo Studentinfo { get; set; }
    }
    public class Studentinfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<CoursesDto>? Courses { get; set; }
    }
    public class CoursesDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<AssignmentsDto>? Assignments { get; set; }
    }
    public class AssignmentsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Point { get; set; }
        public float? AchievedPoint { get; set; }
        public float? Percentage { get; set; }
        public List<CommentsDto>? Comments { get; set; }
    }

    public class CommentsDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}
