using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models
{
    public class StudentId
    {
        public int Id { get; set; }
        public List<int> CourseId { get; set; }
        public List<int> AssessmentId { get; set; }
    }
}
