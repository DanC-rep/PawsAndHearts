using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

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

        builder.OwnsOne(v => v.SocialNetworks, snb =>
        {
            snb.ToJson("social_networks");

            snb.OwnsMany(sn => sn.Values, sb =>
            {
                sb.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_NAME_LENGTH);

                sb.Property(s => s.Link)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TEXT_LENGTH);
            });
        });
        
        builder.OwnsOne(v => v.Requisites, reb =>
        {
            reb.ToJson("requisites");

            reb.OwnsMany(re => re.Values, rb =>
            {
                rb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_NAME_LENGTH);

                rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TEXT_LENGTH);
            });
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}