using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Application.Models;

namespace PawsAndHearts.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;

    public GetVolunteersWithPaginationHandler(
        IReadDbContext readDbContext, 
        ILogger<GetVolunteersWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        var keySelector = SortByProperty(query.SortBy);

        volunteersQuery = query.SortDirection?.ToLower() == "desc"
            ? volunteersQuery.OrderByDescending(keySelector)
            : volunteersQuery.OrderBy(keySelector);
        
        var result = await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
        
        _logger.LogInformation("Volunteers was received with count: {totalCount}", result.Items.Count);

        return result;
    }

    private static Expression<Func<VolunteerDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return volunteer => volunteer.Id;

        Expression<Func<VolunteerDto, object>> keySelector = sortBy?.ToLower() switch
        {
            "name" => volunteer => volunteer.Name,
            "surname" => volunteer => volunteer.Surname,
            "patronymic" => volunteer => volunteer.Patronymic ?? string.Empty,
            "experience" => volunteer => volunteer.Experience,
            _ => volunteer => volunteer.Id
        };
        
        return keySelector;
    }
}