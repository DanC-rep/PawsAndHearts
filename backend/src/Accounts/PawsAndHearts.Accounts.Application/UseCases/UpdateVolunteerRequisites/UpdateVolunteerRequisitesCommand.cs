using PawsAndHearts.Accounts.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.Accounts.Application.UseCases.UpdateVolunteerRequisites;

public record UpdateVolunteerRequisitesCommand(Guid UserId, IEnumerable<RequisiteDto> Requisites) : ICommand
{
    public static UpdateVolunteerRequisitesCommand Create(
        UpdateVolunteerRequisitesRequest request, Guid userId) =>
        new(userId, request.Requisites);
}