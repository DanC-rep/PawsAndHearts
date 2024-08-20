namespace PawsAndHearts.Domain.Shared;

public abstract class Entity<TId> where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }
    
    public TId Id { get; }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (Entity<TId>)obj;

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().FullName + Id).GetHashCode();
    }

    public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
    {
        if (a is null && b is null)
            return true;
        
        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId>? a, Entity<TId>? b)
    {
        return !(a == b);
    }
}