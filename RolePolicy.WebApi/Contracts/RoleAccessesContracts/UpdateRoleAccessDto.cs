namespace RolePolicy.WebApi.Contracts.RoleAccessesContracts;

public record UpdateRoleAccessDto(
    int Id,
    int? UserId,
    int? RoleId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn);