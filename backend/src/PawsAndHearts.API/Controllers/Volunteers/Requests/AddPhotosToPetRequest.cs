namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record AddPhotosToPetRequest(Guid PetId, IFormFileCollection Files);