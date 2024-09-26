using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<SpeciesDto> Species { get; }
    
    IQueryable<BreedDto> Breeds { get; }
    
    IQueryable<PetDto> Pets { get; }
}