using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Queries.GetUserList;

public class GetUserListQueryHandler(IUserRepository userRepository, ILogger<GetUserListQueryHandler> logger) : IRequestHandler<GetUserListQuery, Result<List<User>>>
{
    public async Task<Result<List<User>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка пользователей из базы данных.");
        var records = await userRepository.GetAllAsync();
        if (records.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка пользователей из базы данных.", records.Error.Code);
            return Result.Failure<List<User>>(records.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return records;
    }
}
