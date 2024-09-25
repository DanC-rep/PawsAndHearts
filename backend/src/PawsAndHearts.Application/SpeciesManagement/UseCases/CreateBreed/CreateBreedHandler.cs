using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Species.Entities;

namespace PawsAndHearts.Application.SpeciesManagement.UseCases.CreateBreed;

public class CreateBreedHandler : ICommandHandler<Guid, CreateBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateBreedCommand> _validator;
    private readonly ILogger<CreateBreedHandler> _logger;

    public CreateBreedHandler(
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
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

        speciesResult.Value.AddBreed(breed);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed was created with id {breedId}", (Guid)breedId);

        return (Guid)breedId;
    }

}