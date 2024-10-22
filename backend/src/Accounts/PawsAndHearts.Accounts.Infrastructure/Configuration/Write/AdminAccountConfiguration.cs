using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Accounts.Domain;

namespace PawsAndHearts.Accounts.Infrastructure.Configuration.Write;

public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");
        
        builder.HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<AdminAccount>(v => v.UserId);
    }
}