using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Auths
{
    public class RegisterUserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
