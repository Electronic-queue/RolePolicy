namespace RolePolicy.WebApi.Contracts.ResourcesContracts;

public record UpdateResourceDto(
    int Id,
    string? Name,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn);