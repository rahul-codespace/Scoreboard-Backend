using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StreamId { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assessment> Assessments { get; set; }
        public List<StudentAssessment> StudentAssessments { get; set; }
    }
}
