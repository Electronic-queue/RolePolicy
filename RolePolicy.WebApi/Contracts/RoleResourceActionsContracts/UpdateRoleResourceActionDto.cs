namespace RolePolicy.WebApi.Contracts.RoleResourceActionsContracts;

public record UpdateRoleResourceActionDto(
    int Id,
    int? RoleId,
    int? ResourceId,
    int? ActionId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn);