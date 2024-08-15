using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

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

    public Result<FullName> Create(string name, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<FullName>("Name can not be empty");
        }
        else if (string.IsNullOrWhiteSpace(surname))
        {
            return Result.Failure<FullName>("Surname can not be empty");
        }

        return new FullName(name, surname, patronymic);
    }
}