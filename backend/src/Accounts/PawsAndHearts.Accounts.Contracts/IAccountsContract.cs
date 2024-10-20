using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.Accounts.Contracts;

public interface IAccountsContract
{
    Task<Result<IEnumerable<string>, ErrorList>> GetPermissionsByUserId(Guid userId);
}