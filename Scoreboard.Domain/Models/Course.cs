using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<StreamCourses> Streams { get; set; }
    public List<Assessment> Assessments { get; set; }
}
