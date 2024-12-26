using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionById;

public record GetRoleResourceActionByIdQuery(int Id) : IRequest<Result<RoleResourceAction>>;
