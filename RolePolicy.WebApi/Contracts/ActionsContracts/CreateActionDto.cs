namespace RolePolicy.WebApi.Contracts.ActionsContracts;

public record CreateActionDto(
    string Name,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy);