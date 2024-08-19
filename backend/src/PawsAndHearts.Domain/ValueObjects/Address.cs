using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

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

    public static Result<Address, Error> Create(string city, string street, string house, string? flat)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired("city");

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired("street");

        if (string.IsNullOrWhiteSpace(house))
            return Errors.General.ValueIsRequired("house");

        return new Address(city, street, house, flat);
    }
}