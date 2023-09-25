using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Students
{
    public class RegisterStudentDto
    {
        public required int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public required int StreamId { get; set; }
    }
}
