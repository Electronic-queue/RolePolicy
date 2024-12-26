using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.RoleAccesses.Commands.UpdateRoleAccess;

public record UpdateRoleAccessCommand(
    int Id,
    int? UserId = null,
    int? RoleId = null,
    string? DescriptionRu = null,
    string? DescriptionKk = null,
    string? DescriptionEn = null) : IRequest<Result>;
