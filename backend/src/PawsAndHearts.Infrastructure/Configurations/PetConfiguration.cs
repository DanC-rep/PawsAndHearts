using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_NAME_LENGTH);

        builder.Property(p => p.Species)
            .IsRequired()
            .HasMaxLength(Constants.MAX_TEXT_LENGTH);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH);

        builder.Property(p => p.Breed)
            .IsRequired()
            .HasMaxLength(Constants.MAX_TEXT_LENGTH);

        builder.Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(Constants.MAX_TEXT_LENGTH);

        builder.Property(p => p.HealthInfo)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HEATH_INFO_LENGTH);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(Constants.MAX_ADDRESS_LENGTH);

        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Height)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(Constants.MAX_PHONE_LENGTH);

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.IsVaccinated)
            .IsRequired();

        builder.Property(p => p.HelpStatus)
            .IsRequired();

        builder.HasMany(p => p.Requisites)
            .WithOne()
            .HasForeignKey("pet_id");

        builder.Property(p => p.CreationDate)
            .IsRequired();

        builder.HasMany(p => p.Photos)
            .WithOne()
            .HasForeignKey("pet_id");
    }
}