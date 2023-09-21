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
        public string Name { get; set; }
        public float? Point { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public List<StudentAssessment>? Students { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
