using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.RoleResourceActions.Commands.DeleteRoleResourceAction;

public record DeleteRoleResourceActionCommand(int Id) : IRequest<Result>;
