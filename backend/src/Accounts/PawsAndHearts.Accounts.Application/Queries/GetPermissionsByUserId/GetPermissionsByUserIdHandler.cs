using CSharpFunctionalExtensions;
using PawsAndHearts.Accounts.Application.Interfaces;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.Accounts.Application.Queries.GetPermissionsByUserId;

public class GetPermissionsByUserIdHandler : IQueryHandlerWithResult<IEnumerable<string>, GetPermissionsByUserIdQuery>
{
    private readonly IPermissionManager _permissionManager;


    public GetPermissionsByUserIdHandler(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task<Result<IEnumerable<string>, ErrorList>> Handle(
        GetPermissionsByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await _permissionManager.GetPermissionsByUserId(query.UserId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        return result.Value.ToList();
    }
}