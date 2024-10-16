using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Accounts.Domain;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.Accounts.Application.UseCases.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        UserManager<User> userManager,
        ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = command.Email,
            UserName = command.UserName
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description));
            return new ErrorList(errors);
        }
        
        _logger.LogInformation("User {userName} was successfully created", command.UserName);
        return Result.Success<ErrorList>();
    }
}