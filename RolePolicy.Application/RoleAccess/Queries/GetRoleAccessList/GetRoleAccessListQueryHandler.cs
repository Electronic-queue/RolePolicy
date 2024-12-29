using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessList;

public class GetRoleAccessListQueryHandler(IRoleAccessRepository roleAccessRepository, ILogger<GetRoleAccessListQueryHandler> logger) : IRequestHandler<GetRoleAccessListQuery, Result<List<RoleAccess>>>
{
    public async Task<Result<List<RoleAccess>>> Handle(GetRoleAccessListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка предоставлений роли из базы данных.");
        var records = await roleAccessRepository.GetAllAsync();
        if (records.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка предоставлений роли из базы данных.", records.Error.Code);
            return Result.Failure<List<RoleAccess>>(records.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return records;
    }
}
