using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.DeleteVolunteer;

public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly ILogger<DelegatingHandler> _logger;

    public DeleteVolunteerHandler(IVolunteersRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<DeleteVolunteerCommand> validator,
        ILogger<DelegatingHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        volunteerResult.Value.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Volunteer was deleted with id {volunteerId}", command.VolunteerId);

        return (Guid)volunteerResult.Value.Id;
    }
}