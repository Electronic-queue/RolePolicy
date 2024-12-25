using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionById;

public class GetActionByIdQueryHandler(IActionRepository actionRepository) : IRequestHandler<GetActionByIdQuery, Result<Action>>
{
    public async Task<Result<Action>> Handle(GetActionByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await actionRepository.GetById(request.Id);
        if (record == null)
        {
            return Result.Failure<Action>(new Error(Errors.BadRequest, $"Ошибка при получении сущности с id {request.Id} из таблицы Actions."));
        }
        return record;
    }
}
