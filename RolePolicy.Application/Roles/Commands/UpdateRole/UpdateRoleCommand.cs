using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(
    int Id,
    string? NameRu = null,
    string? NameKk = null,
    string? NameEn = null,
    string? DescriptionRu = null,
    string? DescriptionKk = null,
    string? DescriptionEn = null) : IRequest<Result>;
