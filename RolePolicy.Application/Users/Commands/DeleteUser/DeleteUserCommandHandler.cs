using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository UserRepository, ILogger<DeleteUserCommandHandler> logger) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на удаление пользователя из базы данных.");
        var result =  await UserRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление пользователя из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}