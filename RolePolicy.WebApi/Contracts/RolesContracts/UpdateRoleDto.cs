namespace RolePolicy.WebApi.Contracts.RolesContracts;

public record UpdateRoleDto(
    int Id,
    string? NameRu,
    string? NameKk,
    string? NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn);