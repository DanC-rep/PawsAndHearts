using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;
using PawsAndHearts.Infrastructure.Extensions;

namespace PawsAndHearts.Infrastructure.Configurations.Write;

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
                .HasMaxLength(Domain.Shared.Constants.MAX_NAME_LENGTH)
                .HasColumnName("name");

            fb.Property(f => f.Surname)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_NAME_LENGTH)
                .HasColumnName("surname");

            fb.Property(f => f.Patronymic)
                .IsRequired(false)
                .HasMaxLength(Domain.Shared.Constants.MAX_NAME_LENGTH)
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
                .HasMaxLength(Domain.Shared.Constants.MAX_PHONE_LENGTH)
                .HasColumnName("phone_number");
        });
        
        builder.Property(v => v.Requisites)
            .HasValueObjectsJsonConversion(
                requisite => new RequisiteDto(requisite.Name, requisite.Description),
                dto => Requisite.Create(dto.Name, dto.Description).Value)
            .HasColumnName("requisites");

        builder.Property(v => v.SocialNetworks)
            .HasValueObjectsJsonConversion(
                socialNetwork => new SocialNetworkDto(socialNetwork.Name, socialNetwork.Link),
                dto => SocialNetwork.Create(dto.Link, dto.Name).Value)
            .HasColumnName("social_networks");

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}