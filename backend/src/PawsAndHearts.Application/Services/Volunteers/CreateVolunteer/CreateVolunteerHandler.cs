using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerId = VolunteerId.NewId();
        
        var fullName = FullName.Create(
            command.FullName.Name, 
            command.FullName.Surname, 
            command.FullName.Patronymic)
            .Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var experience = Experience.Create(command.Experience).Value;

        var socialNetworks = command.SocialNetworks.Select(s =>
                SocialNetwork.Create(s.Name, s.Link).Value).ToList();

        var requisites = command.Requisites.Select(r =>
                Requisite.Create(r.Name, r.Description).Value).ToList();

        var volunteerResult = new Volunteer(
            volunteerId, 
            fullName,
            experience,
            phoneNumber, 
            socialNetworks,
            requisites);

        await _volunteersRepository.Add(volunteerResult, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Created volunteer with id {volunteerId}", (Guid)volunteerId);

        return (Guid)volunteerResult.Id;
    }
}