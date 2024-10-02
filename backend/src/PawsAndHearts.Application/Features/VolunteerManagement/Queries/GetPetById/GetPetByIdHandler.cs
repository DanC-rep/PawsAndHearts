using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetPetById;

public class GetPetByIdHandler : IQueryHandlerWithResult<PetDto, GetPetByIdQuery>
{
    private readonly IReadDbContext _context;

    public GetPetByIdHandler(IReadDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PetDto, ErrorList>> Handle(
        GetPetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var petDto = await _context.Pets.FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (petDto is null)
            return Errors.General.NotFound(query.Id).ToErrorList();

        return petDto;
    }
}