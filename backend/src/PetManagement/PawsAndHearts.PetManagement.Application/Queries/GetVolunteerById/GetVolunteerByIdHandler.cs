using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.PetManagement.Application.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler : IQueryHandlerWithResult<VolunteerDto, GetVolunteerByIdQuery>
{
    private readonly IVolunteersReadDbContext _volunteersReadDbContext;
    public GetVolunteerByIdHandler(IVolunteersReadDbContext volunteersReadDbContext)
    {
        _volunteersReadDbContext = volunteersReadDbContext;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteerDto = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync(
            v => v.Id == query.Id, cancellationToken);

        if (volunteerDto is null)
            return Errors.General.NotFound(query.Id).ToErrorList();

        return volunteerDto;
    }
}