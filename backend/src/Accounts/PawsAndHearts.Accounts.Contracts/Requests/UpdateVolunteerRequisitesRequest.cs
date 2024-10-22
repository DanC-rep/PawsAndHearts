using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.Accounts.Contracts.Requests;

public record UpdateVolunteerRequisitesRequest(IEnumerable<RequisiteDto> Requisites);