using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Accounts.Domain;
using PawsAndHearts.Accounts.Infrastructure.IdentityManagers;
using PawsAndHearts.Accounts.Infrastructure.Options;

namespace PawsAndHearts.Accounts.Infrastructure.Seeding;

public class AccountsSeederService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly PermissionManager _permissionManager;
    private readonly RolePermissionManager _rolePermissionManager;
    private readonly ILogger<AccountsSeederService> _logger;
    
    public AccountsSeederService(
        RoleManager<Role> roleManager,
        PermissionManager permissionManager,
        RolePermissionManager rolePermissionManager,
        ILogger<AccountsSeederService> logger)
    {
        _roleManager = roleManager;
        _permissionManager = permissionManager;
        _rolePermissionManager = rolePermissionManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        var json = await File.ReadAllTextAsync(SharedKernel.Constants.FilePaths.ACCOUNTS);

        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
            ?? throw new ApplicationException("Could not deserialize role permission config");

        await SeedPermissions(seedData, _permissionManager);

        await SeedRoles(seedData, _roleManager);

        await SeedRolePermissions(seedData, _roleManager, _rolePermissionManager);
    }

    private async Task SeedPermissions(RolePermissionOptions seedData, PermissionManager permissionManager)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionGroup => permissionGroup.Value);

        await permissionManager.AddRangeIfExists(permissionsToAdd);
        
        _logger.LogInformation("Permissions added to database");
    }

    private async Task SeedRoles(RolePermissionOptions seedData, RoleManager<Role> roleManager)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await roleManager.FindByNameAsync(role);

            if (existingRole is null)
                await roleManager.CreateAsync(new Role { Name = role });
        }
        
        _logger.LogInformation("Roles added to database");
    }

    private async Task SeedRolePermissions(
        RolePermissionOptions seedData, 
        RoleManager<Role> roleManager,
        RolePermissionManager rolePermissionManager)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role is null)
                throw new ApplicationException($"Role {roleName} not found");
            
            await rolePermissionManager.AddRangeIfNotExists(role.Id, seedData.Roles[roleName]);
        }
        
        _logger.LogInformation("Role permissions added to database");
    }
}