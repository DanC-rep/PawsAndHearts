using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.PetManagement.Application;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Domain.Entities;
using PawsAndHearts.PetManagement.Infrastructure.DbContexts;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.PetManagement.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly WriteDbContext _writeDbContext;

    public VolunteersRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _writeDbContext.AddAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId, 
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _writeDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);

        return volunteer;
    }
}