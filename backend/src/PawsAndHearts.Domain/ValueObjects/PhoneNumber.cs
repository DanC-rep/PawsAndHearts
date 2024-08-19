using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex PhoneNumberRegex = new Regex(
        "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$"); 
    
    private PhoneNumber(string value)
    {
        Value = value;
    }
    
    public string Value { get; } = default!;

    public static Result<PhoneNumber, Error> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Errors.General.ValueIsRequired("phone number");

        if (!PhoneNumberRegex.IsMatch(phoneNumber))
            return Errors.General.ValueIsInvalid("phone number");

        return new PhoneNumber(phoneNumber);
    }
}