using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value).Value);

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

        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH);

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH);

            ab.Property(a => a.House)
                .IsRequired()
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH);

            ab.Property(a => a.Flat)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH);
        });

        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Height)
            .IsRequired();

        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_LENGTH);
        });

        builder.ComplexProperty(p => p.BirthDate, bb =>
        {
            bb.Property(b => b.Value)
                .IsRequired();
        });

        builder.Property(p => p.IsVaccinated)
            .IsRequired();

        builder.Property(p => p.HelpStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.ComplexProperty(p => p.CreationDate, cb =>
        {
            cb.Property(c => c.Value)
                .IsRequired();
        });

        builder.OwnsOne(p => p.PetDetails, peB =>
        {
            peB.ToJson();
            
            peB.OwnsMany(d => d.Photos, phB =>
            {
                phB.Property(p => p.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);

                phB.Property(p => p.IsMain)
                    .IsRequired();
            });
            
            peB.OwnsMany(d => d.Requisites, rb =>
            {
                rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);

                rb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_NAME_LENGTH);
            });
        });
    }
}