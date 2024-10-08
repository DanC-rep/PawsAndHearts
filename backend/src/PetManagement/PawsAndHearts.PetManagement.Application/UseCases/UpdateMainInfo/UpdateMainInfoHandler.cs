using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateMainInfo;

public class UpdateMainInfoHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
{
    private readonly IVolunteersRepository _repository;
    private readonly IPetManagementUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository repository,
        IPetManagementUnitOfWork unitOfWork,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var fullName = FullName.Create(
            command.FullName.Name,
            command.FullName.Surname,
            command.FullName.Patronymic).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var experience = Experience.Create(command.Experience).Value;
        
        volunteerResult.Value.UpdateMainInfo(fullName, phoneNumber, experience);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Updated volunteer with id {volunteerId}", command.VolunteerId);

        return (Guid)volunteerResult.Value.Id;
    }
}