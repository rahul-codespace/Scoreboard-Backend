using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models;

public class StreamCourse
{
    public int StreamId { get; set; }
    public int CourseId { get; set; }
    public Stream? Stream { get; set; }
    public Course? Course { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
