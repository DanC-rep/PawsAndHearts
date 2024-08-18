using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record Address
{
    private Address(string city, string street, string house, string? flat)
    {
        City = city;
        Street = street;
        House = house;
        flat = flat;
    }
    
    public string City { get; } = default!;

    public string Street { get; } = default!;

    public string House { get; } = default!;

    public string? Flat { get; } = default!;

    public static Result<Address> Create(string city, string street, string house, string? flat)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return Result.Failure<Address>("City can not be empty");
        }
        else if (string.IsNullOrWhiteSpace(street))
        {
            return Result.Failure<Address>("Street can not be empty");
        }
        else if (string.IsNullOrWhiteSpace(house))
        {
            return Result.Failure<Address>("House can not be empty");
        }

        return new Address(city, street, house, flat);
    }
}