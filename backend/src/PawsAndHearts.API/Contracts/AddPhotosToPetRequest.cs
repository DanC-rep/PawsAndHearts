namespace PawsAndHearts.API.Contracts;

public record AddPhotosToPetRequest(Guid PetId, IFormFileCollection Files);