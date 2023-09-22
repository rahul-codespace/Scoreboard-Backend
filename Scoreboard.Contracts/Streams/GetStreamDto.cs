using Scoreboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Streams
{
    public class GetStreamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        // Define a navigation property to represent the relationship to Students
        public ICollection<Student> Students { get; set; }

        // Define a navigation property to represent the relationship to StreamCourses
        public ICollection<StreamCourse> StreamCourses { get; set; }
    }
}
