using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Accounts.Domain;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Infrastructure.Configuration.Write;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");

        builder.ComplexProperty(v => v.Experience, eb =>
        {
            eb.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("experience");
        });
        
        builder.Property(v => v.Requisites)
            .HasValueObjectsJsonConversion(
                requisite => new RequisiteDto(requisite.Name, requisite.Description),
                dto => Requisite.Create(dto.Name, dto.Description).Value)
            .HasColumnName("requisites");

        builder.HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<VolunteerAccount>(v => v.UserId);
    }
}