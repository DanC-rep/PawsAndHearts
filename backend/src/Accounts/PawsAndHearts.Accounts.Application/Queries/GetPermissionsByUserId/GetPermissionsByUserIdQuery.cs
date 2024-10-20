using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.Accounts.Application.Queries.GetPermissionsByUserId;

public record GetPermissionsByUserIdQuery(Guid UserId) : IQuery;