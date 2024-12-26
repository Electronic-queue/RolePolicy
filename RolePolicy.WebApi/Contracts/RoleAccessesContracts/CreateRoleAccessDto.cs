namespace RolePolicy.WebApi.Contracts.RoleAccessesContracts;

public record CreateRoleAccessDto(
    int UserId,
    int RoleId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int GivenBy);
