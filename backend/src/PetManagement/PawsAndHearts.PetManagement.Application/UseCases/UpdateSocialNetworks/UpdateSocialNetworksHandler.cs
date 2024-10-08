using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler : ICommandHandler<Guid, UpdateSocialNetworksCommand>
{
    private readonly IVolunteersRepository _repository;
    private readonly IPetManagementUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly ILogger<UpdateSocialNetworksHandler> _logger;
    
    public UpdateSocialNetworksHandler(
        IVolunteersRepository repository, 
        IPetManagementUnitOfWork unitOfWork,
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