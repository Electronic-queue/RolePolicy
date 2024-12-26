using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.RoleResourceActions.Commands.UpdateRoleResourceAction;

public record UpdateRoleResourceActionCommand(
    int Id,
    int? RoleId,
    int? ResourceId,
    int? ActionId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn) : IRequest<Result>;
