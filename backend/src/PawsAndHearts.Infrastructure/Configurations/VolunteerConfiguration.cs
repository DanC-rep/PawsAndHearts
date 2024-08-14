using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.Shared;

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
                value => BaseId.Create(value));

        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_NAME_LENGTH);

        builder.Property(v => v.Surname)
            .IsRequired()
            .HasMaxLength(Constants.MAX_NAME_LENGTH);

        builder.Property(v => v.Patronymic)
            .IsRequired(false)
            .HasMaxLength(Constants.MAX_NAME_LENGTH);

        builder.Property(v => v.Experience)
            .IsRequired()
            .HasMaxLength(Constants.MAX_EXPERIENCE_VALUE);

        builder.Property(v => v.PetsFoundHome)
            .IsRequired();

        builder.Property(v => v.PetsLookingForHome)
            .IsRequired();

        builder.Property(v => v.PetsBeingTreated)
            .IsRequired();

        builder.Property(v => v.Phone)
            .IsRequired()
            .HasMaxLength(Constants.MAX_PHONE_LENGTH);

        builder.HasMany(v => v.SocialNetworks)
            .WithOne()
            .HasForeignKey(s => s.VolunteerId);

        builder.HasMany(v => v.Requisites)
            .WithOne()
            .HasForeignKey(r => r.VolunteerId);

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}