using System.Linq.Expressions;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.Core.Models;
using PawsAndHearts.PetManagement.Application.Interfaces;

namespace PawsAndHearts.PetManagement.Application.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IVolunteersReadDbContext _volunteersReadDbContext;
    public GetVolunteersWithPaginationHandler(IVolunteersReadDbContext volunteersReadDbContext)
    {
        _volunteersReadDbContext = volunteersReadDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _volunteersReadDbContext.Volunteers;

        var keySelector = SortByProperty(query.SortBy);

        volunteersQuery = query.SortDirection?.ToLower() == "desc"
            ? volunteersQuery.OrderByDescending(keySelector)
            : volunteersQuery.OrderBy(keySelector);
        
        var result = await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

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