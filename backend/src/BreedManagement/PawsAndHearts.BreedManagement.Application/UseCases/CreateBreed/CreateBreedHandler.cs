using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.BreedManagement.Domain.Entities;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateBreed;

public class CreateBreedHandler : ICommandHandler<Guid, CreateBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IBreedManagementUnitOfWork _unitOfWork;
    private readonly IValidator<CreateBreedCommand> _validator;
    private readonly ILogger<CreateBreedHandler> _logger;

    public CreateBreedHandler(
        ISpeciesRepository speciesRepository,
        IBreedManagementUnitOfWork unitOfWork,
        IValidator<CreateBreedCommand> validator,
        ILogger<CreateBreedHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var speciesResult = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedId = BreedId.NewId();

        var breed = new Breed(breedId, command.Name, command.SpeciesId);

        var addBreedResult = speciesResult.Value.AddBreed(breed);

        if (addBreedResult.IsFailure)
            return addBreedResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed was created with id {breedId}", (Guid)breedId);

        return (Guid)breedId;
    }

}