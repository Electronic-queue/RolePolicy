namespace RolePolicy.WebApi.Contracts.ActionsContracts;

public record UpdateActionDto(
    int Id,
    string? Name,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn);