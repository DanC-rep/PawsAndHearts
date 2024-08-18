using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record PhoneNumber
{
    private PhoneNumber(string value)
    {
        Value = value;
    }
    
    public string Value { get; } = default!;

    public static Result<PhoneNumber> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result.Failure<PhoneNumber>("Phone number can not be empty");
        }

        return new PhoneNumber(phoneNumber);
    }
}