using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessList;

public record GetRoleAccessListQuery() : IRequest<Result<List<RoleAccess>>>;
