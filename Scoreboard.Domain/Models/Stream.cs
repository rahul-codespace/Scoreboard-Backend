using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models;

public class Stream
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student>? Students { get; set; }
    public List<StreamCourse>? StreamCourses { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
