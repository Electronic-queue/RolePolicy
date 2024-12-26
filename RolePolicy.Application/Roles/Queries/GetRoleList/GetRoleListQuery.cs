using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Roles.Queries.GetRoleList;

public record GetRoleListQuery() : IRequest<Result<List<Role>>>;
