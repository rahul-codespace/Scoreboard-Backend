using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string ReviewerName { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string FeedBackPoints { get; set; }
        public int Rating { get; set; }

    }
}
