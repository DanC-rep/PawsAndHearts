using PawsAndHearts.Accounts.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.Accounts.Application.UseCases.Register;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand
{
    public static RegisterUserCommand Create(RegisterUserRequest request) =>
        new(request.Email, request.UserName, request.Password);
}