namespace PawsAndHearts.Core.Options;

public class AdminOptions
{
    public const string ADMIN = "Admin";

    public string UserName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string Password { get; init; } = default!;
}