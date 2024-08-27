using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly ILogger<UpdateSocialNetworksHandler> _logger;
    
    public UpdateSocialNetworksHandler(
        IVolunteersRepository repository, 
        ILogger<UpdateSocialNetworksHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateSocialNetworksRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var socialNetworks = new SocialNetworks(
            request.Dto.SocialNetworks.Select(s =>
                SocialNetwork.Create(s.Name, s.Link).Value).ToList());
        
        volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

        var result = await _repository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Social networks were updated for volunteer with id {volunteerId}",
            request.VolunteerId);

        return result;
    }
}