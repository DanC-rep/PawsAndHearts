using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Services.Volunteers.Delete;

public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly ILogger<DelegatingHandler> _logger;

    public DeleteVolunteerHandler(IVolunteersRepository repository, ILogger<DelegatingHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var result = await _repository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Volunteer was deleted with id {volunteerId}", request.VolunteerId);

        return result;
    }
}