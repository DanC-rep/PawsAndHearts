using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.Accounts.Application.UseCases.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;