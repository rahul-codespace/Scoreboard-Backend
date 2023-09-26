using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Scoreboards
{
    public class StudentInfoDto
    {
        public int Rank { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Domain.Models.Stream Stream { get; set; }
        public double Percentage { get; set; }
    }
}
