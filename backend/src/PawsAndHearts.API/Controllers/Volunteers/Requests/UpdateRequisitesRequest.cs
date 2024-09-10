using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites);