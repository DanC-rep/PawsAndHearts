using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Domain.ValueObjects;

public class PetPhoto : ValueObject
{
    private PetPhoto(FilePath path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }
    
    public FilePath Path { get; }
    
    public bool IsMain { get; }

    public static Result<PetPhoto, Error> Create(FilePath path, bool isMain)
    {
        return new PetPhoto(path, isMain);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Path;
        yield return IsMain;
    }
}