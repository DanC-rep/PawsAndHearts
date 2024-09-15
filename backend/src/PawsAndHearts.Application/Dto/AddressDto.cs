namespace PawsAndHearts.Application.Dto;

public record AddressDto(string City, string Street, string House, string? Flat = null);