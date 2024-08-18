using CSharpFunctionalExtensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, string>> Handle(CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewId();
        
        var fullNameResult = FullName.Create(request.Name, request.Surname, request.Patronymic);

        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var socialNetworks = new List<SocialNetwork>();

        if (request.SocialNetworks != null)
        {
            foreach (var socialNetworkDto in request.SocialNetworks)
            {
                var socialNetworkResult = SocialNetwork.Create(
                    socialNetworkDto.Name,
                    socialNetworkDto.Link);

                if (socialNetworkResult.IsFailure)
                    return socialNetworkResult.Error;

                socialNetworks.Add(socialNetworkResult.Value);
            }
        }
        
        var requisites = new List<Requisite>();

        if (request.Requisites != null)
        {
            foreach (var requisiteDto in request.Requisites)
            {
                var requisiteResult = Requisite.Create(
                    requisiteDto.Name,
                    requisiteDto.Description);

                if (requisiteResult.IsFailure)
                    return requisiteResult.Error;

                requisites.Add(requisiteResult.Value);
            }
        }
        
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