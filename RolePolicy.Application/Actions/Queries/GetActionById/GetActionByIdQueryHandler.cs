using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionById;

public class GetActionByIdQueryHandler(IActionRepository actionRepository, ILogger<GetActionByIdQueryHandler> logger) : IRequestHandler<GetActionByIdQuery, Result<Action>>
{
    public async Task<Result<Action>> Handle(GetActionByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение действия из базы данных.");
        var record = await actionRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение действия из базы данных.", record.Error.Code);
            return Result.Failure<Action>(record.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return record;
    }
}
