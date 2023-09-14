using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models
{
    public class StudentTotalPoint
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public float TotalPoints { get; set; }
        public float TotalAchievedPoints { get; set; }
        public float PercentageScore { get; set; }
    }
}
