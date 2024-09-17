using PawsAndHearts.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new (Page, PageSize);
}