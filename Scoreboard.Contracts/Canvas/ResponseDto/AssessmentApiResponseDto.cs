using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Canvas.ResponseDto
{
    public class AssessmentApiResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Points_possible { get; set; }
        public int Course_id { get; set; }
    }
}
