using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Accounts.Domain;

namespace PawsAndHearts.Accounts.Infrastructure.IdentityManagers;

public class RolePermissionManager
{
    private readonly AccountsDbContext _accountsDbContext;
    private readonly RoleManager<Role> _roleManager;
    
    public RolePermissionManager(
        AccountsDbContext accountsDbContext,
        RoleManager<Role> roleManager)
    {
        _accountsDbContext = accountsDbContext;
        _roleManager = roleManager;
    }

    public async Task AddRangeIfNotExists(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await _accountsDbContext.Permissions
                .FirstOrDefaultAsync(p => p.Code == permissionCode);
            
            if (permission is null)
                throw new ApplicationException($"Permission code {permissionCode} is not found");
            
            var rolePermissionExists = await _accountsDbContext.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id);
            
            if (rolePermissionExists)
                continue;

            await _accountsDbContext.RolePermissions.AddAsync(
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permission.Id
                });
        }

        await _accountsDbContext.SaveChangesAsync();
    }
}