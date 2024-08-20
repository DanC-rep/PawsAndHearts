using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork(string link, string name)
    {
        Link = link;
        Name = name;
    }
    
    public string Link { get; } = default!;

    public string Name { get; } = default!;

    public static Result<SocialNetwork, Error> Create(string link, string name)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsRequired("link");

        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("name");
                
        return new SocialNetwork(link, name);
    }
}