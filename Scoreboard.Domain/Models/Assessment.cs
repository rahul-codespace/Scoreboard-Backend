using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Scoreboard.Domain.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? points_possible { get; set; }
        public float? score { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<StudentAssesment> Students { get; set; }
    }

    public class Grades
    {
        public float? score { get; set; }
    }
}
