using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Application.Interfaces;

public interface IPetRepository
{
    void Delete(Pet pet);
}