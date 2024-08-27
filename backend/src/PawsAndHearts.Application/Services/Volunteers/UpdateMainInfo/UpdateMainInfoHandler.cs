using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository repository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var fullName = FullName.Create(
            request.Dto.FullName.Name,
            request.Dto.FullName.Surname,
            request.Dto.FullName.Patronymic).Value;

        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;

        var experience = Experience.Create(request.Dto.Experience).Value;
        
        volunteerResult.Value.UpdateMainInfo(fullName, phoneNumber, experience);

        var result = await _repository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Updated volunteer with id {volunteerId}", request.VolunteerId);

        return result;
    }
}