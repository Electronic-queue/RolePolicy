using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler(IRoleRepository roleRepository, ILogger<GetRoleByIdQueryHandler> logger) : IRequestHandler<GetRoleByIdQuery, Result<Role>>
{
    public async Task<Result<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение роли из базы данных.");
        var record = await roleRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение роли из базы данных.", record.Error.Code);
            return Result.Failure<Role>(record.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return record;
    }
}
