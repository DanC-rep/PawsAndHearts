using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

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

        builder.ComplexProperty(v => v.VolunteerPetsMetrics, vb =>
        {
            vb.Property(vp => vp.PetsFoundHome)
                .IsRequired()
                .HasColumnName("pets_found_home");

            vb.Property(vp => vp.PetsLookingForHome)
                .IsRequired()
                .HasColumnName("pets_looking_for_home");

            vb.Property(vp => vp.PetsBeingTreated)
                .IsRequired()
                .HasColumnName("pets_being_treated");
        });

        builder.ComplexProperty(v => v.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.OwnsOne(v => v.SocialNetworks, snb =>
        {
            snb.ToJson();

            snb.OwnsMany(sn => sn.Value, sb =>
            {
                sb.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_NAME_LENGTH);

                sb.Property(s => s.Link)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);
            }).ToJson("social_networks");
        });
        
        builder.OwnsOne(v => v.Requisites, reb =>
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

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}