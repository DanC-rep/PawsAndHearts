using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.Accounts.Application.Interfaces;

public interface IPermissionManager
{
    Task<Result<IEnumerable<string>, Error>> GetPermissionsByUserId(
        Guid userId, 
        CancellationToken cancellationToken = default);
}