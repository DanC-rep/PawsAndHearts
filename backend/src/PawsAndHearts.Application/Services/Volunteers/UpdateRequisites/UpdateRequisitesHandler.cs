using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;

public class UpdateRequisitesHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly ILogger<UpdateRequisitesHandler> _logger;

    public UpdateRequisitesHandler(
        IVolunteersRepository repository, 
        ILogger<UpdateRequisitesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateRequisitesRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var requisites = new Requisites(
            request.Dto.Requisites.Select(r =>
                Requisite.Create(r.Name, r.Description).Value).ToList());

        volunteerResult.Value.UpdateRequisites(requisites);

        var result = await _repository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Requisites were updated for volunteer with id {volunteerId}",
            request.VolunteerId);

        return result;
    }
}