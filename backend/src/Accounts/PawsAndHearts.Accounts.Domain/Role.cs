﻿using Microsoft.AspNetCore.Identity;

namespace PawsAndHearts.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; }

    public List<User> Users { get; set; } = [];
}