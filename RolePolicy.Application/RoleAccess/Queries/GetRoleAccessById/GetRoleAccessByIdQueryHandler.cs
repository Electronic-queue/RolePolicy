using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessById;

public class GetRoleAccessByIdQueryHandler(IRoleAccessRepository roleAccessRepository, ILogger<GetRoleAccessByIdQueryHandler> logger) : IRequestHandler<GetRoleAccessByIdQuery, Result<RoleAccess>>
{
    public async Task<Result<RoleAccess>> Handle(GetRoleAccessByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение предоставление роли из базы данных.");
        var record = await roleAccessRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение предоставление роли из базы данных.", record.Error.Code);
            return Result.Failure<RoleAccess>(record.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return record;
    }
}
