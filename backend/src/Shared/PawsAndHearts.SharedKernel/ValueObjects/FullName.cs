using CSharpFunctionalExtensions;

namespace PawsAndHearts.SharedKernel.ValueObjects;

public class FullName : ValueObject
{
    private FullName(string name, string surname, string? patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }

    private FullName()
    {
        
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

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        yield return Patronymic ?? string.Empty;
    }
}