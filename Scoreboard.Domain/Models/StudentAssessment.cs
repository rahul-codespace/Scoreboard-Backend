﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models
{
    public class StudentAssessment
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int AssessmentId { get; set; }
        public Assessment? Assessment { get; set; }
        public float? AchievedPoints { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
