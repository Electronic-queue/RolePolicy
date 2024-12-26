using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(int Id) : IRequest<Result>;
