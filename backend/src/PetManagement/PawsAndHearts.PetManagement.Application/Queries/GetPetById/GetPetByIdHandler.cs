using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Contracts.Dtos;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.PetManagement.Application.Queries.GetPetById;

public class GetPetByIdHandler : IQueryHandlerWithResult<PetDto, GetPetByIdQuery>
{
    private readonly IVolunteersReadDbContext _context;

    public GetPetByIdHandler(IVolunteersReadDbContext context)
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