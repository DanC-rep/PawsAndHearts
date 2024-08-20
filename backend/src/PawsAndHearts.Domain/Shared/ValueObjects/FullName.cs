using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record FullName
{
    private FullName(string name, string surname, string? patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }
    
    public string Name { get; } = default!;

    public string Surname { get; } = default!;

    public string? Patronymic { get; } = default!;

    public static Result<FullName, Error> Create(string name, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("name");

        if (string.IsNullOrWhiteSpace(surname))
            return Errors.General.ValueIsRequired("surname");

        return new FullName(name, surname, patronymic);
    }
}