using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly ILogger<UpdateSocialNetworksHandler> _logger;
    
    public UpdateSocialNetworksHandler(
        IVolunteersRepository repository, 
        IUnitOfWork unitOfWork,
        IValidator<UpdateSocialNetworksCommand> validator,
        ILogger<UpdateSocialNetworksHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var socialNetworks = command.SocialNetworks.Select(s =>
                SocialNetwork.Create(s.Name, s.Link).Value).ToList();
        
        volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Social networks were updated for volunteer with id {volunteerId}",
            command.VolunteerId);

        return (Guid)volunteerResult.Value.Id;
    }
}