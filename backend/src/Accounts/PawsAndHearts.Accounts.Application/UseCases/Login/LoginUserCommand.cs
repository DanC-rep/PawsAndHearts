using PawsAndHearts.Accounts.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.Accounts.Application.UseCases.Login;

public record LoginUserCommand(string Email, string Password) : ICommand
{
    public static LoginUserCommand Create(LoginUserRequest request) =>
        new(request.Email, request.Password);
}