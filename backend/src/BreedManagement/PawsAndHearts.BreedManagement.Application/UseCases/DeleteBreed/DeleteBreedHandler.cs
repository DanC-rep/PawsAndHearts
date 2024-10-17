using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Enums;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Contracts;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.UseCases.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly IPetManagementContract _petManagementContract;
    private readonly ISpeciesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IValidator<DeleteBreedCommand> _validator;

    public DeleteBreedHandler(
        IPetManagementContract petManagementContract,
        ISpeciesRepository repository,
        [FromKeyedServices(Modules.BreedManagement)] IUnitOfWork unitOfWork,
        ILogger<DeleteBreedHandler> logger,
        IValidator<DeleteBreedCommand> validator)
    {
        _petManagementContract = petManagementContract;
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

        var petHaveNotBreedResult = await _petManagementContract
            .CheckPetsDoNotHaveBreed(breedResult.Value.Id, cancellationToken);

        if (petHaveNotBreedResult.IsFailure)
            return petHaveNotBreedResult.Error.ToErrorList();

        speciesResult.Value.RemoveBreed(breedResult.Value);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed was deleted with id {breedId}", command.BreedId);

        return (Guid)breedResult.Value.Id;
    }
}