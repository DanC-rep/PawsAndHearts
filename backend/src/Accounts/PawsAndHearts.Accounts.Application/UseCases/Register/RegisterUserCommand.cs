using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.Accounts.Application.UseCases.Register;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;