using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Interfaces;

public interface IReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get; }
}