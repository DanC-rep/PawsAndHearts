using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Domain.Entities;
using PawsAndHearts.PetManagement.Infrastructure.DbContexts;

namespace PawsAndHearts.PetManagement.Infrastructure.Repositories;

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