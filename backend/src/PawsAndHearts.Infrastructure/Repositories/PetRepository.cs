using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Volunteer.Entities;
using PawsAndHearts.Infrastructure.DbContexts;

namespace PawsAndHearts.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    private readonly WriteDbContext _context;

    public PetRepository(WriteDbContext context)
    {
        _context = context;
    }
    
    public void Delete(Pet pet)
    {
        _context.Remove(pet);
    }
}