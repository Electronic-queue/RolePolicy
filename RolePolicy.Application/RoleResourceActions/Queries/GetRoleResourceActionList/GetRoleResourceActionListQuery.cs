using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionList;

public record GetRoleResourceActionListQuery() : IRequest<Result<List<RoleResourceAction>>>;
