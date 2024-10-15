using PawsAndHearts.Accounts.Application.UseCases.Register;

namespace PawsAndHearts.Accounts.Presentation.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand() =>
        new(Email, UserName, Password);
}