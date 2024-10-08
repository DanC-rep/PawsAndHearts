using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PawsAndHearts.Core.Extensions;

public static class FluentApiExtensions
{
    public static PropertyBuilder<IReadOnlyList<TValueObject>> HasValueObjectsJsonConversion<TValueObject, TDto>
        (this PropertyBuilder<IReadOnlyList<TValueObject>> builder,
            Func<TValueObject, TDto> toDtoSelect,
            Func<TDto, TValueObject> toValueObjectSelector)
    {
        return builder.HasConversion(
                valueObjects => SerializeValueObjectsCollection(valueObjects, toDtoSelect),
                json => DeserializeDtosCollection(json, toValueObjectSelector),
                CreateCollectionValueComparer<TValueObject>())
            .HasColumnType("jsonb");
    }

    private static string SerializeValueObjectsCollection<TValueObject, TDto>(
        IReadOnlyList<TValueObject> items, Func<TValueObject, TDto> selector)
    {
        var dtos = items.Select(selector);

        return JsonSerializer.Serialize(dtos, JsonSerializerOptions.Default);
    }

    private static IReadOnlyList<TValueObject> DeserializeDtosCollection<TValueObject, TDto>(
        string json, Func<TDto, TValueObject> selector)
    {
        var dtos = JsonSerializer.Deserialize<IEnumerable<TDto>>(json, JsonSerializerOptions.Default) ?? [];

        return dtos.Select(selector).ToList();
    }

    private static ValueComparer<IReadOnlyList<T>> CreateCollectionValueComparer<T>()
    {
        return new ValueComparer<IReadOnlyList<T>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
            c => c.ToList());
    }
}