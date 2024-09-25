using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.SpeciesManagement.UseCases.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly ISpeciesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IValidator<DeleteBreedCommand> _validator;

    public DeleteBreedHandler(
        ISpeciesRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteBreedHandler> logger,
        IValidator<DeleteBreedCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var speciesResult = await _repository.GetById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedResult = speciesResult.Value.GetBreedById(command.BreedId);

        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();
        
        breedResult.Value.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed was deleted with id {breedId}", command.BreedId);

        return (Guid)breedResult.Value.Id;
    }
}