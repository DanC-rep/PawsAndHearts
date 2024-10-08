using CSharpFunctionalExtensions;

namespace PawsAndHearts.SharedKernel.ValueObjects;

public class Address : ValueObject
{
    private Address(string city, string street, string house, string? flat)
    {
        City = city;
        Street = street;
        House = house;
        Flat = flat;
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

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return House;
        yield return Flat ?? string.Empty;
    }
}