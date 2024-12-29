using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionById;

public class GetRoleResourceActionByIdQueryHandler(IRoleResourceActionRepository roleResourceActionRepository, ILogger<GetRoleResourceActionByIdQueryHandler> logger) : IRequestHandler<GetRoleResourceActionByIdQuery, Result<RoleResourceAction>>
{
    public async Task<Result<RoleResourceAction>> Handle(GetRoleResourceActionByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение связи роли и действия над ресурсом из базы данных.");
        var record = await roleResourceActionRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение связи роли и действия над ресурсом из базы данных.", record.Error.Code);
            return Result.Failure<RoleResourceAction>(record.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return record;
    }
}
