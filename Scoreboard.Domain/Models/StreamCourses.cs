﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models
{
    public class StreamCourses
    {
        public int Id { get; set; }
        public int StreamId { get; set; }
        public int CourseId { get; set; }
        public Stream Stream { get; set; }
        public Course Course { get; set; }
    }
}
