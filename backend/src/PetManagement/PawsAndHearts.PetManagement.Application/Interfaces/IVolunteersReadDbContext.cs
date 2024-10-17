using PawsAndHearts.PetManagement.Contracts.Dtos;

namespace PawsAndHearts.PetManagement.Application.Interfaces;

public interface IVolunteersReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<PetDto> Pets { get; }
}