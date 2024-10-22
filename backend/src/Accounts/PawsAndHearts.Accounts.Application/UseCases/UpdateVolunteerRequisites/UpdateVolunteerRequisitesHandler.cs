using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Accounts.Application.Interfaces;
using PawsAndHearts.Accounts.Domain;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Enums;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Application.UseCases.UpdateVolunteerRequisites;

public class UpdateVolunteerRequisitesHandler : ICommandHandler<UpdateVolunteerRequisitesCommand>
{
    private readonly IAccountManager _accountManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerRequisitesCommand> _validator;
    private readonly ILogger<UpdateVolunteerRequisitesHandler> _logger;

    public UpdateVolunteerRequisitesHandler(
        IAccountManager accountManager,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerRequisitesCommand> validator,
        ILogger<UpdateVolunteerRequisitesHandler> logger)
    {
        _accountManager = accountManager;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        UpdateVolunteerRequisitesCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerAccount = await _accountManager
            .GetVolunteerAccountByUserId(command.UserId, cancellationToken);

        if (volunteerAccount.IsFailure)
            return volunteerAccount.Error.ToErrorList();

        var requisites = command.Requisites.Select(r =>
            Requisite.Create(r.Name, r.Description).Value)
            .ToList();

        volunteerAccount.Value.UpdateRequisites(requisites);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Requisites was updated for user {userName}", 
            volunteerAccount.Value.User.UserName);

        return Result.Success<ErrorList>();
    }
}