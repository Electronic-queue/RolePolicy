using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionList;

public class GetActionListQueryHandler(IActionRepository actionRepository, ILogger<GetActionListQueryHandler> logger) : IRequestHandler<GetActionListQuery, Result<List<Action>>>
{
    public async Task<Result<List<Action>>> Handle(GetActionListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка действий из базы данных.");
        var records = await actionRepository.GetAllAsync();
        if (records.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение действия.", records.Error.Code);
            return Result.Failure<List<Action>>(records.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return records;
    }
}
