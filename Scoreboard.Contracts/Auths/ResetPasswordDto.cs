using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Auths
{
    public class ResetPasswordDto
    {
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
