using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Infrastructure.Configurations;

public class RequisiteConfiguration : IEntityTypeConfiguration<Requisite>
{
    public void Configure(EntityTypeBuilder<Requisite> builder)
    {
        builder.ToTable("requisites");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_NAME_LENGTH);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_TEXT_LENGTH);
    }
}