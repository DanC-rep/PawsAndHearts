﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Species.Entities;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IValidator<DeleteBreedCommand> _validator;

    public DeleteBreedHandler(
        IReadDbContext readDbContext,
        ISpeciesRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteBreedHandler> logger,
        IValidator<DeleteBreedCommand> validator)
    {
        _readDbContext = readDbContext;
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

        var petHaveNotBreedResult = await CheckPetsDoNotHaveBreed(breedResult.Value);

        if (petHaveNotBreedResult.IsFailure)
            return petHaveNotBreedResult.Error.ToErrorList();

        speciesResult.Value.RemoveBreed(breedResult.Value);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Breed was deleted with id {breedId}", command.BreedId);

        return (Guid)breedResult.Value.Id;
    }

    private async Task<UnitResult<Error>> CheckPetsDoNotHaveBreed(Breed breed)
    {
        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(p => p.BreedId == (Guid)breed.Id);

        if (pet is null)
            return Result.Success<Error>();
        
        return Errors.General.AlreadyUsed(breed.Id);
    }
}