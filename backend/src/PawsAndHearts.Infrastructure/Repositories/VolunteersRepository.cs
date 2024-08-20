using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly PawsAndHeartsDbContext _pawsAndHeartsDbContext;

    public VolunteersRepository(PawsAndHeartsDbContext pawsAndHeartsDbContext)
    {
        _pawsAndHeartsDbContext = pawsAndHeartsDbContext;
    }
    
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _pawsAndHeartsDbContext.AddAsync(volunteer, cancellationToken);

        await _pawsAndHeartsDbContext.SaveChangesAsync();

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId, 
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _pawsAndHeartsDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId);

        if (volunteer is null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }
}