using Scoreboard.Shared.Auths;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Students
{
    public class RegisterStudentDto
    {
        [EmailAddress(ErrorMessage = AuthConsts.EmailErrorMessage)]
        public required string Email { get; set; }

        [RegularExpression(AuthConsts.PasswordRegex, ErrorMessage = AuthConsts.PasswordRegexErrorMessage)]
        public string Password { get; set; }
        public required int StreamId { get; set; }
    }
}
