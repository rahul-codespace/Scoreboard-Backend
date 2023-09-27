using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Shared.Auths
{
    public class AuthConsts
    {
        public const string NameRegexErrorMessage = "Name can only contain letters.";

        public const string NameRegex = @"^[a-zA-Z]+$";
        public const string EmailErrorMessage = "Invalid email address.";
        public const string PasswordRegex = @"^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        public const string PasswordRegexErrorMessage = "Password must be 8 characters including one uppercase letter, one special character and alphanumeric characters";
    }
}
