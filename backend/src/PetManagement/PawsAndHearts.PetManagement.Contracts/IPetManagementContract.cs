using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.PetManagement.Contracts;

public interface IPetManagementContract
{
    Task<UnitResult<Error>> CheckPetsDoNotHaveBreed(
        Guid breedId, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> CheckPetsDoNotHaveSpecies(
        Guid speciesId, CancellationToken cancellationToken = default);
}