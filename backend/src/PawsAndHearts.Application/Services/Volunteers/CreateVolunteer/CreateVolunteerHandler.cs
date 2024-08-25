using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewId();
        
        var fullName = FullName.Create(
            request.FullName.Name, 
            request.FullName.Surname, 
            request.FullName.Patronymic)
            .Value;

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var experience = Experience.Create(request.Experience).Value;

        var socialNetworks = new SocialNetworks(
            request.SocialNetworks?.Select(s =>
                SocialNetwork.Create(s.Name, s.Link).Value).ToList());

        var requisites = new Requisites(
            request.Requisites?.Select(r =>
                Requisite.Create(r.Name, r.Description).Value).ToList());

        var volunteerResult = new Volunteer(
            volunteerId, 
            fullName,
            experience,
            phoneNumber, 
            socialNetworks,
            requisites);

        await _volunteersRepository.Add(volunteerResult, cancellationToken);
        
        _logger.LogInformation("Created volunteer with id {volunteerId}", (Guid)volunteerId);

        return (Guid)volunteerResult.Id;
    }
}