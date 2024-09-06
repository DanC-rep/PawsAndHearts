using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public record PetMetrics
{
    public PetMetrics(double height, double weight)
    {
        Height = height;
        Weight = weight;
    }
    
    public double Height { get; } = default!;
    public double Weight { get; } = default!;

    public static Result<PetMetrics, Error> Create(double height, double weight)
    {
        if (height < 0)
            return Errors.General.ValueIsInvalid("height");

        if (weight < 0)
            return Errors.General.ValueIsInvalid("weight");

        return new PetMetrics(height, weight);
    }
}