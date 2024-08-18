using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Application.Interfaces;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Result<Volunteer>> GetById(VolunteerId volunteerId);
}