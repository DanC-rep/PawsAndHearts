using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PawsAndHearts.SharedKernel.ValueObjects;

public class PhoneNumber : ValueObject
{
    private static readonly Regex PhoneNumberRegex = new Regex(
        "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$"); 
    
    private PhoneNumber(string value)
    {
        Value = value;
    }
    
    public string Value { get; } = default!;

    public static Result<PhoneNumber, Error> Create(string input)
    {
        var phoneNumber = input.Trim();

        if (!PhoneNumberRegex.IsMatch(phoneNumber))
            return Errors.General.ValueIsInvalid("phone number");

        return new PhoneNumber(phoneNumber);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}