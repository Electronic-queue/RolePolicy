namespace RolePolicy.WebApi.Contracts.ResourcesContracts;

public record CreateResourceDto(
    string Name,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy);