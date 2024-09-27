using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;
using PawsAndHearts.Domain.Volunteer.ValueObjects;
using PawsAndHearts.Infrastructure.Extensions;

namespace PawsAndHearts.Infrastructure.Configurations.Write;

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
            .HasMaxLength(Domain.Shared.Constants.MAX_NAME_LENGTH);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_DESCRIPTION_LENGTH);
        
        builder.ComplexProperty(p => p.Position, cb =>
        {
            cb.Property(c => c.Value)
                .IsRequired()
                .HasColumnName("position");
        });

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

        builder.ComplexProperty(p => p.Color, cb =>
        {
            cb.Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_TEXT_LENGTH)
                .HasColumnName("color");
        });

        builder.Property(p => p.HealthInfo)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_HEATH_INFO_LENGTH);

        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("city");

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("street");

            ab.Property(a => a.House)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("house");

            ab.Property(a => a.Flat)
                .IsRequired(false)
                .HasMaxLength(Domain.Shared.Constants.MAX_ADDRESS_LENGTH)
                .HasColumnName("flat");
        });

        builder.ComplexProperty(p => p.PetMetrics, mb =>
        {
            mb.Property(m => m.Height)
                .IsRequired()
                .HasColumnName("height");

            mb.Property(m => m.Weight)
                .IsRequired()
                .HasColumnName("weight");
        });

        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_PHONE_LENGTH)
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
        
        builder.Property(v => v.Requisites)
            .HasValueObjectsJsonConversion(
                requisite => new RequisiteDto(requisite.Name, requisite.Description),
                dto => Requisite.Create(dto.Name, dto.Description).Value)
            .HasColumnName("requisites");

        builder.Property(v => v.PetPhotos)!
            .HasValueObjectsJsonConversion(
                petPhoto => new PetPhotoDto(petPhoto.Path.Path),
                dto => PetPhoto.Create(FilePath.Create(dto.PathToStorage).Value, false).Value)
            .HasColumnName("pet_photos");
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}