namespace RolePolicy.WebApi.Contracts.RoleResourceActionsContracts;

public record CreateRoleResourceActionDto(
    int RoleId,
    int ResourceId,
    int ActionId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy);
