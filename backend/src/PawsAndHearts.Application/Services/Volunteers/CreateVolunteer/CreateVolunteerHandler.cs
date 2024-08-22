using CSharpFunctionalExtensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewId();
        
        var fullName = FullName.Create(request.Name, request.Surname, request.Patronymic).Value;

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var socialNetworks = new SocialNetworks(
            request.SocialNetworks?.Select(s =>
                SocialNetwork.Create(s.Name, s.Link).Value).ToList());

        var requisites = new Requisites(
            request.Requisites?.Select(r =>
                Requisite.Create(r.Name, r.Description).Value).ToList());

        var volunteerResult = new Volunteer(
            volunteerId, 
            fullName,
            request.Experience,
            phoneNumber, 
            socialNetworks,
            requisites);

        await _volunteersRepository.Add(volunteerResult, cancellationToken);

        return (Guid)volunteerResult.Id;
    }
}