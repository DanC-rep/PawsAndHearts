using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public record Position
{
    public static Position First => new (1);
    
    private Position(int value)
    {
        Value = value;
    }
    
    public int Value { get; }

    public Result<Position, Error> Forward()
        => Create(Value + 1); 
    
    public Result<Position, Error> Back()
        => Create(Value - 1); 

    public static Result<Position, Error> Create(int position)
    {
        if (position <= 0)
            return Errors.General.ValueIsInvalid("position");
        
        return new Position(position);
    }
}