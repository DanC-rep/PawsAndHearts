﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using PawsAndHearts.Accounts.Application.Interfaces;
using PawsAndHearts.Accounts.Domain;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.Accounts.Application.UseCases.Login;

public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        ILogger<LoginUserHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is null)
            return Errors.General.NotFound(null, "user").ToErrorList();
        
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);

        if (!passwordConfirmed)
            return Errors.Accounts.InvalidCredentials().ToErrorList();

        var token = _tokenProvider.GenerateAccessToken(user, cancellationToken);
        
        _logger.LogInformation("Successfully logged in");

        return token;
    }
}