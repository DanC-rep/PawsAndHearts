using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler : IQueryHandlerWithResult<VolunteerDto, GetVolunteerByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    public GetVolunteerByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteerDto = await _readDbContext.Volunteers.FirstOrDefaultAsync(
            v => v.Id == query.Id, cancellationToken);

        if (volunteerDto is null)
            return Errors.General.NotFound(query.Id).ToErrorList();

        return volunteerDto;
    }
}