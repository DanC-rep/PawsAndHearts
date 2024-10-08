using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesById;

public class GetSpeciesByIdHandler : IQueryHandlerWithResult<SpeciesDto, GetSpeciesByIdQuery>
{
    private readonly ISpeciesReadDbContext _readDbContext;
    
    public GetSpeciesByIdHandler(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<SpeciesDto, ErrorList>> Handle(
        GetSpeciesByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var speciesDto = await _readDbContext.Species
            .FirstOrDefaultAsync(s => s.Id == query.SpeciesId, cancellationToken);

        if (speciesDto is null)
            return Errors.General.NotFound(query.SpeciesId).ToErrorList();

        return speciesDto;
    }
}