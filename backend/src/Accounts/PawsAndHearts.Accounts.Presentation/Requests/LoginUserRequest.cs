using PawsAndHearts.Accounts.Application.UseCases.Login;

namespace PawsAndHearts.Accounts.Presentation.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public LoginUserCommand ToCommand() =>
        new(Email, Password);
}