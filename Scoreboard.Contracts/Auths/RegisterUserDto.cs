using Scoreboard.Shared.Auths;
using System.ComponentModel.DataAnnotations;
namespace Scoreboard.Contracts.Auths;

public class RegisterUserDto
{
    [RegularExpression(AuthConsts.NameRegex, ErrorMessage = AuthConsts.NameRegexErrorMessage)]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = AuthConsts.EmailErrorMessage)]
    public string Email { get; set; }
    [RegularExpression(AuthConsts.PasswordRegex, ErrorMessage = AuthConsts.PasswordRegexErrorMessage)]
    public required string Password { get; set; }
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public required string ConfirmPassword { get; set; }
}
