using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public class BirthDate : ValueObject
{
    private BirthDate(DateTime value)
    {
        Value = value;
    }
    
    public DateTime Value { get; }

    public static Result<BirthDate, Error> Create(DateTime birthDate)
    {
        if (birthDate > DateTime.Now || birthDate.Year < 1900)
            return Errors.General.ValueIsInvalid("birth date");
        
        return new BirthDate(birthDate);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}