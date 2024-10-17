using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Dtos;

namespace PawsAndHearts.PetManagement.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(p => p.Id);

        builder.HasQueryFilter(v => !v.IsDeleted);
        
        builder.Property(v => v.Requisites)
            .HasConversion(
                values => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<RequisiteDto>>
                    (json, JsonSerializerOptions.Default)!);
        
        builder.Property(v => v.PetPhotos)
            .HasConversion(
                values => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<PetPhotoDto>>
                    (json, JsonSerializerOptions.Default)!);
    }
}