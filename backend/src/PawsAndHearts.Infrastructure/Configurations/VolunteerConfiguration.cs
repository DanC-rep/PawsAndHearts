using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Infrastructure.Configurations;

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
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("name");

            fb.Property(f => f.Surname)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("surname");

            fb.Property(f => f.Patronymic)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("patronymic");
        });

        builder.Property(v => v.Experience)
            .IsRequired()
            .HasMaxLength(Constants.MAX_EXPERIENCE_VALUE);

        builder.Property(v => v.PetsFoundHome)
            .IsRequired();

        builder.Property(v => v.PetsLookingForHome)
            .IsRequired();

        builder.Property(v => v.PetsBeingTreated)
            .IsRequired();

        builder.ComplexProperty(v => v.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.OwnsOne(v => v.VolunteerDetails, vb =>
        {
            vb.ToJson();

            vb.OwnsMany(d => d.SocialNetworks, sb =>
            {
                sb.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_NAME_LENGTH);

                sb.Property(s => s.Link)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);
            });

            vb.OwnsMany(d => d.Requisites, rb =>
            {
                rb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_NAME_LENGTH);

                rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);
            });
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}