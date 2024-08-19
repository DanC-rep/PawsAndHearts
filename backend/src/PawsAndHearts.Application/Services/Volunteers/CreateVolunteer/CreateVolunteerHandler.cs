using CSharpFunctionalExtensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewId();
        
        var fullNameResult = FullName.Create(request.Name, request.Surname, request.Patronymic);

        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var socialNetworksResult = request.SocialNetworks?.Select(s =>
            SocialNetwork.Create(s.Name, s.Link)).ToList();

        if (socialNetworksResult != null && socialNetworksResult.Any(s => s.IsFailure))
            return Errors.General.ValueIsRequired("name or link in social network");

        var socialNetworks = socialNetworksResult?.Select(s => 
                s.Value).ToList();

        var requisitesResult = request.Requisites?.Select(r =>
            Requisite.Create(r.Name, r.Description)).ToList();

        if (requisitesResult != null && requisitesResult.Any(r => r.IsFailure))
            return Errors.General.ValueIsRequired("name or description in requisite");

        var requisites = requisitesResult?.Select(r => 
                r.Value).ToList();
        
        var volunteerDetailsResult = VolunteerDetails.Create(socialNetworks, requisites);

        if (volunteerDetailsResult.IsFailure)
            return volunteerDetailsResult.Error;

        var volunteerResult = Volunteer.Create(
            volunteerId, 
            fullNameResult.Value,
            request.Experience, 
            request.PetsFoundHome, 
            request.PetsLookingForHome,
            request.PetsBeingTreated, 
            phoneNumberResult.Value, 
            volunteerDetailsResult.Value);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        await _volunteersRepository.Add(volunteerResult.Value, cancellationToken);

        return (Guid)volunteerResult.Value.Id;
    }
}