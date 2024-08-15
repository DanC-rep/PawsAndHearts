using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork(string link, string name)
    {
        Link = link;
        Name = name;
    }
    
    public string Link { get; } = default!;

    public string Name { get; } = default!;

    public static Result<SocialNetwork> Create(string link, string name)
    {
        if (string.IsNullOrWhiteSpace(link) || string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<SocialNetwork>("Link or name can not be empty");
        }

        return new SocialNetwork(link, name);
    }
}