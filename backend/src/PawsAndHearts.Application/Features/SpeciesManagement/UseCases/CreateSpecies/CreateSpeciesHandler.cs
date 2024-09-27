using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Species.Entities;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateSpecies;

public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpeciesCommand> _validator;
    private readonly ILogger<CreateSpeciesHandler> _logger;

    public CreateSpeciesHandler(
        IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateSpeciesCommand> validator,
        ILogger<CreateSpeciesHandler> logger)
    {
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSpeciesCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var speciesId = SpeciesId.NewId();

        var species = new Species(speciesId, command.Name);
        
        var speciesNameNotExistsResult = await CheckSpeciesNameNotExists(species);

        if (speciesNameNotExistsResult.IsFailure)
            return speciesNameNotExistsResult.Error.ToErrorList();
        
        var result = await _speciesRepository.Add(species, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Species was created with id {speciesId}", (Guid)speciesId);

        return result;
    }

    private async Task<UnitResult<Error>> CheckSpeciesNameNotExists(Species species)
    {
        var foundedSpecies = await _readDbContext.Species
            .FirstOrDefaultAsync(s => s.Name == species.Name);

        if (foundedSpecies is null)
            return Result.Success<Error>();

        return Errors.General.AlreadyExists("species", "name", species.Name);
    }
        
}