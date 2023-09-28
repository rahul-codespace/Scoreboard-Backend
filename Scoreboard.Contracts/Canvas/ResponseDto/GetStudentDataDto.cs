using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Canvas.ResponseDto
{
    public class GetStudentDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("login_id")]
        public string Email { get; set; }
    }
}
