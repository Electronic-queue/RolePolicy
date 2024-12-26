using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.RoleResourceActions.Commands.CreateRoleResourceAction;

public record CreateRoleResourceActionCommand(
    int RoleId,
    int ResourceId,
    int ActionId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy) : IRequest<Result>;
