using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.RoleAccesses.Commands.DeleteRoleAccess;

public record DeleteRoleAccessCommand(int Id) : IRequest<Result>;
