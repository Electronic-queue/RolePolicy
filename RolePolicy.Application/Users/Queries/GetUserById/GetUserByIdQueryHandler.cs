using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository UserRepository, ILogger<GetUserByIdQueryHandler> logger) : IRequestHandler<GetUserByIdQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение пользователя из базы данных.");
        var record = await UserRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение пользователя из базы данных.", record.Error.Code);
            return Result.Failure<User>(record.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return record;
    }
}
