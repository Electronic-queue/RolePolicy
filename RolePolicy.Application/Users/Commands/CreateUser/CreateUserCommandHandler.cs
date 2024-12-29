using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository UserRepository, ILogger<CreateUserCommandHandler> logger) : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на создание нового пользователя в базе данных.");
        var user = new Domain.Entities.User {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Surname = request.Surname,
            Login = request.Login,
            PasswordHash = request.PasswordHash,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await UserRepository.AddAsync(user);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового пользователя в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}