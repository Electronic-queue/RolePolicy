using KDS.Primitives.FluentResult;
using MediatR;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionById;

public record GetActionByIdQuery(int Id) : IRequest<Result<Action>>;
