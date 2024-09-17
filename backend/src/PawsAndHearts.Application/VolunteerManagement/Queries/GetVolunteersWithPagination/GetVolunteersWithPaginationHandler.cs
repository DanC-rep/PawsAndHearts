using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Application.Models;

namespace PawsAndHearts.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers.AsQueryable();

        return await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}