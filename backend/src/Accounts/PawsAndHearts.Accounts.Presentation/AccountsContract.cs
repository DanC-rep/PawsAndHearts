using CSharpFunctionalExtensions;
using PawsAndHearts.Accounts.Application.Queries.GetPermissionsByUserId;
using PawsAndHearts.Accounts.Contracts;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly GetPermissionsByUserIdHandler _getPermissionsByUserIdHandler;

    public AccountsContract(GetPermissionsByUserIdHandler getPermissionsByUserIdHandler)
    {
        _getPermissionsByUserIdHandler = getPermissionsByUserIdHandler;
    }


    public async Task<Result<IEnumerable<string>, ErrorList>> GetPermissionsByUserId(Guid userId)
    {
        var query = new GetPermissionsByUserIdQuery(userId);
        
        return await _getPermissionsByUserIdHandler.Handle(query);
    }
}