using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Species.Entities;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteSpecies;

public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;

    public DeleteSpeciesHandler(
        IReadDbContext readDbContext,
        ISpeciesRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesHandler> logger)
    {
        _readDbContext = readDbContext;
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

        var petsHaveNotSpeciesResult = await CheckPetsDoNotHaveSpecies(speciesResult.Value);

        if (petsHaveNotSpeciesResult.IsFailure)
            return petsHaveNotSpeciesResult.Error.ToErrorList();

        var result = _repository.Delete(speciesResult.Value);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Species was deleted with id {speciesId}", command.SpeciesId);
        
        return result.Value;
    }

    private async Task<UnitResult<Error>> CheckPetsDoNotHaveSpecies(Species species)
    {
        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(p => p.SpeciesId == (Guid)species.Id);

        if (pet is null)
            return Result.Success<Error>();

        return Errors.General.AlreadyUsed(species.Id);
    }
        
}