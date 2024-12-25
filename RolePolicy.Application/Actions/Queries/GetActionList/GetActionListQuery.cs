using KDS.Primitives.FluentResult;
using MediatR;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionList;

public record GetActionListQuery() : IRequest<Result<List<Action>>>;
