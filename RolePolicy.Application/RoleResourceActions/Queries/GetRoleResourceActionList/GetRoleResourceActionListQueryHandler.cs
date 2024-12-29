using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionList;

public class GetRoleResourceActionListQueryHandler(IRoleResourceActionRepository roleResourceActionRepository, ILogger<GetRoleResourceActionListQueryHandler> logger) : IRequestHandler<GetRoleResourceActionListQuery, Result<List<RoleResourceAction>>>
{
    public async Task<Result<List<RoleResourceAction>>> Handle(GetRoleResourceActionListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка связей роли и действия над ресурсом из базы данных.");
        var records = await roleResourceActionRepository.GetAllAsync();
        if (records.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка связей роли и действия над ресурсом из базы данных.", records.Error.Code);
            return Result.Failure<List<RoleResourceAction>>(records.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return records;
    }
}
