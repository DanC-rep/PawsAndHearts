using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Domain.Entities;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.PetManagement.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);
        
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(SharedKernel.Constants.MAX_NAME_LENGTH)
                .HasColumnName("name");

            fb.Property(f => f.Surname)
                .IsRequired()
                .HasMaxLength(SharedKernel.Constants.MAX_NAME_LENGTH)
                .HasColumnName("surname");

            fb.Property(f => f.Patronymic)
                .IsRequired(false)
                .HasMaxLength(SharedKernel.Constants.MAX_NAME_LENGTH)
                .HasColumnName("patronymic");
        });

        builder.ComplexProperty(v => v.Experience, eb =>
        {
            eb.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("experience");
        });

        builder.ComplexProperty(v => v.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(SharedKernel.Constants.MAX_PHONE_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);

        builder.Property(v => v.IsDeleted)
            .HasColumnName("is_deleted");
    }
}