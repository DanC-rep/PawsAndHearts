using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites);