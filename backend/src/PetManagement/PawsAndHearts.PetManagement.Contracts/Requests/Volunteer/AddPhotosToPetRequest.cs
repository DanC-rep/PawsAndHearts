using Microsoft.AspNetCore.Http;

namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record AddPhotosToPetRequest(IFormFileCollection Files);