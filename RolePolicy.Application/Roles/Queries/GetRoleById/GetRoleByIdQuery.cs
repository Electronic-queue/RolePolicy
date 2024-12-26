using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(int Id) : IRequest<Result<Role>>;
