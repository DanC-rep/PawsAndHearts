using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Accounts.Domain;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Enums;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Application.UseCases.UpdateUserSocialNetworks;

public class UpdateUserSocialNetworksHandler : ICommandHandler<UpdateUserSocialNetworksCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateUserSocialNetworksCommand> _validator;
    private readonly ILogger<UpdateUserSocialNetworksHandler> _logger;

    public UpdateUserSocialNetworksHandler(
        UserManager<User> userManager,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        IValidator<UpdateUserSocialNetworksCommand> validator,
        ILogger<UpdateUserSocialNetworksHandler> logger)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        UpdateUserSocialNetworksCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
            return Errors.General.NotFound(command.UserId, "user id").ToErrorList();
        
        var socialNetworks = command.SocialNetworks.Select(s =>
            SocialNetwork.Create(s.Name, s.Link).Value).ToList();
        
        user.UpdateSocialNetworks(socialNetworks);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Social networks was updated for user {userName}", user.UserName);

        return Result.Success<ErrorList>();
    }
}