using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Enums;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateRequisites;

public class UpdateRequisitesHandler : ICommandHandler<Guid, UpdateRequisitesCommand>
{
    private readonly IVolunteersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateRequisitesCommand> _validator;
    private readonly ILogger<UpdateRequisitesHandler> _logger;

    public UpdateRequisitesHandler(
        IVolunteersRepository repository, 
        [FromKeyedServices(Modules.PetManagement)] IUnitOfWork unitOfWork, 
        IValidator<UpdateRequisitesCommand> validator,
        ILogger<UpdateRequisitesHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateRequisitesCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var requisites = command.Requisites.Select(r =>
                Requisite.Create(r.Name, r.Description).Value).ToList();

        volunteerResult.Value.UpdateRequisites(requisites);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Requisites were updated for volunteer with id {volunteerId}",
            command.VolunteerId);

        return (Guid)volunteerResult.Value.Id;
    }
}