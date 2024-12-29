using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Queries.GetRoleList;

public class GetRoleListQueryHandler(IRoleRepository roleRepository, ILogger<GetRoleListQueryHandler> logger) : IRequestHandler<GetRoleListQuery, Result<List<Role>>>
{
    public async Task<Result<List<Role>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка ролей из базы данных.");
        var records = await roleRepository.GetAllAsync();
        if (records.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка ролей из базы данных.", records.Error.Code);
            return Result.Failure<List<Role>>(records.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return records;
    }
}
