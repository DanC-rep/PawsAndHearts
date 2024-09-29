using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.Interfaces;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Enums;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Domain.Volunteer.Entities;

public class Pet : Shared.Entity<PetId>, ISoftDeletable
{
    private bool _isDeleted = false;
    
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        string name,
        string description,
        PetIdentity petIdentity,
        Color color, 
        string healthInfo,
        Address address,
        PetMetrics petMetrics,
        PhoneNumber phoneNumber,
        bool isNeutered,
        BirthDate birthDate,
        bool isVaccinated,
        HelpStatus helpStatus,
        CreationDate creationDate,
        ValueObjectList<Requisite> requisites) : base(id)
    {
        Name = name;
        Description = description;
        PetIdentity = petIdentity;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PetMetrics = petMetrics;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        BirthDate = birthDate;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreationDate = creationDate;
        Requisites = requisites;
    }

    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;
    
    public Position Position { get; private set; }
    
    public PetIdentity PetIdentity { get; private set; }

    public Color Color { get; private set; }

    public string HealthInfo { get; private set; } = default!;

    public Address Address { get; private set; }

    public PetMetrics PetMetrics { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }
    
    public bool IsNeutered { get; private set; }
    
    public BirthDate BirthDate { get; private set; }
    
    public bool IsVaccinated { get; private set; }
    
    public HelpStatus HelpStatus { get; private set; }
    
    public CreationDate CreationDate { get; private set; }

    public IReadOnlyList<Requisite> Requisites { get; private set; }
    
    public IReadOnlyList<PetPhoto>? PetPhotos { get; private set; }

    public void SetPosition(Position position) =>
        Position = position;

    public void Delete()
    {
        if (!_isDeleted)
            _isDeleted = true;
    }

    public void AddPhotos(IEnumerable<PetPhoto> petPhotos)
    {
        PetPhotos = petPhotos.ToList();
    }

    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
    }

    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();

        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }
    
    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();

        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> Move(Position newPosition)
    {
        Position = newPosition;

        return Result.Success<Error>();
    }

    public void UpdateInfo(Pet updatedPet)
    {
        Name = updatedPet.Name;
        Description = updatedPet.Description;
        PetIdentity = updatedPet.PetIdentity;
        Color = updatedPet.Color;
        HealthInfo = updatedPet.HealthInfo;
        Address = updatedPet.Address;
        PetMetrics = updatedPet.PetMetrics;
        PhoneNumber = updatedPet.PhoneNumber;
        IsNeutered = updatedPet.IsNeutered;
        BirthDate = updatedPet.BirthDate;
        IsVaccinated = updatedPet.IsVaccinated;
        HelpStatus = updatedPet.HelpStatus;
        CreationDate = updatedPet.CreationDate;
        Requisites = updatedPet.Requisites;
    }

    public void DeletePhotos()
    {
        PetPhotos = [];
    }
}