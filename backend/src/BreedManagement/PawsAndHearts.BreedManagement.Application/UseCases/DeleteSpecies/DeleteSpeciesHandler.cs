using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.BreedManagement.Domain.Entities;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Enums;
using PawsAndHearts.PetManagement.Contracts;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.UseCases.DeleteSpecies;

public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly IPetManagementContract _petManagementContract;
    private readonly ISpeciesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;

    public DeleteSpeciesHandler(
        IPetManagementContract petManagementContract,
        ISpeciesRepository repository,
        [FromKeyedServices(Modules.BreedManagement)] IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesHandler> logger)
    {
        _petManagementContract = petManagementContract;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteSpeciesCommand command,
        CancellationToken cancellationToken = default)
    {
        var speciesResult = await _repository.GetById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var petsHaveNotSpeciesResult = await _petManagementContract
            .CheckPetsDoNotHaveSpecies(speciesResult.Value.Id, cancellationToken);

        if (petsHaveNotSpeciesResult.IsFailure)
            return petsHaveNotSpeciesResult.Error.ToErrorList();

        var result = _repository.Delete(speciesResult.Value);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Species was deleted with id {speciesId}", command.SpeciesId);
        
        return result.Value;
    }
}