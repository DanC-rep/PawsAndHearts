using PawsAndHearts.PetManagement.Domain.Entities;

namespace PawsAndHearts.PetManagement.Application.Interfaces;

public interface IPetRepository
{
    void Delete(Pet pet);
}