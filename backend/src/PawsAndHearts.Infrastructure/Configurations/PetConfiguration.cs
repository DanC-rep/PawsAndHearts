using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

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
                value => PetId.Create(value));

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_NAME_LENGTH);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH);

        builder.ComplexProperty(p => p.PetIdentity, pb =>
        {
            pb.Property(p => p.SpeciesId)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("species_id");

            pb.Property(p => p.BreedId)
                .HasColumnName("breed_id");
        });

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
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("city");

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("street");

            ab.Property(a => a.House)
                .IsRequired()
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("house");

            ab.Property(a => a.Flat)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("flat");
        });

        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Height)
            .IsRequired();

        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.ComplexProperty(p => p.BirthDate, bb =>
        {
            bb.Property(b => b.Value)
                .IsRequired()
                .HasColumnName("birth_date");
        });

        builder.Property(p => p.IsVaccinated)
            .IsRequired();

        builder.Property(p => p.HelpStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.ComplexProperty(p => p.CreationDate, cb =>
        {
            cb.Property(c => c.Value)
                .IsRequired()
                .HasColumnName("creation_date");
        });
        
        builder.OwnsOne(p => p.Requisites, reb =>
        {
            reb.ToJson();

            reb.OwnsMany(re => re.Value, rb =>
            {
                rb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_NAME_LENGTH);

                rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);
            }).ToJson("requisites");
        });
        
        builder.OwnsOne(p => p.PetPhotos, phb =>
        {
            phb.ToJson();

            phb.OwnsMany(p => p.Value, pb =>
            {
                pb.Property(p => p.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);

                pb.Property(p => p.IsMain)
                    .IsRequired();
            }).ToJson("pet_photos"); 
        });
    }
}