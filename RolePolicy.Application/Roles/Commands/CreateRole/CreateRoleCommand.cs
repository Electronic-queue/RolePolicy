using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy) : IRequest<Result>;
