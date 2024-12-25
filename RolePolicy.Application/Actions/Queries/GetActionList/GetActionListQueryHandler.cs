using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionList;

public class GetActionListQueryHandler(IActionRepository actionRepository) : IRequestHandler<GetActionListQuery, Result<List<Action>>>
{
    public async Task<Result<List<Action>>> Handle(GetActionListQuery request, CancellationToken cancellationToken)
    {
        var records = await actionRepository.GetAllAsync();
        if (records == null)
        {
            return Result.Failure<List<Action>>(new Error(Errors.BadRequest, "Ошибка при получении сущностей из таблицы Actions."));
        }
        return records;
    }
}
