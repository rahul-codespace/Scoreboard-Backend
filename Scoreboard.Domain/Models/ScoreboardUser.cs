using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Domain.Models
{
    public class ScoreboardUser : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
