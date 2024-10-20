using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Accounts.Contracts;
using PawsAndHearts.Core.Models;

namespace PawsAndHearts.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permission)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var accountContract = scope.ServiceProvider.GetRequiredService<IAccountsContract>();

        var userIdString = context.User.Claims
            .FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            context.Fail();
            return;
        }

        var permissions = await accountContract.GetPermissionsByUserId(userId);
        
        if (permissions.IsFailure)
            context.Fail();

        if (permissions.Value.Contains(permission.Code))
        {
            context.Succeed(permission);
            return;
        }
        
        context.Fail();
    }
}