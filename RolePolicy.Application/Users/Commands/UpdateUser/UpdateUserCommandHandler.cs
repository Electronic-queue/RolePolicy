using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository UserRepository, ILogger<UpdateUserCommandHandler> logger) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на обновление пользователя в базе данных.");
        var result =  await UserRepository.UpdateAsync(request.Id, request.FirstName, request.LastName, request.Surname, request.Login, request.PasswordHash, request.IsDeleted);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление пользователя в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}