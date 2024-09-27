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
    private readonly ILogger<GetVolunteerByIdHandler> _logger;

    public GetVolunteerByIdHandler(
        IReadDbContext readDbContext,
        ILogger<GetVolunteerByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteerDto = await _readDbContext.Volunteers.FirstOrDefaultAsync(
            v => v.Id == query.Id, cancellationToken);

        if (volunteerDto is null)
            return Errors.General.NotFound(query.Id).ToErrorList();
        
        _logger.LogInformation("Volunteer was found with Id {id}", query.Id);

        return volunteerDto;
    }
}