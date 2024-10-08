using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.BreedManagement.Domain.Entities;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.BreedManagement.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(SharedKernel.Constants.MAX_NAME_LENGTH);
    }
}