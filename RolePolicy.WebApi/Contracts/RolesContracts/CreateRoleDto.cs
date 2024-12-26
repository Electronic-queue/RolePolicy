namespace RolePolicy.WebApi.Contracts.RolesContracts;

public record CreateRoleDto(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy);