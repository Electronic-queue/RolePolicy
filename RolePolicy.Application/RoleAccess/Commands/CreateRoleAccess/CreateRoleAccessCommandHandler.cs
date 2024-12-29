using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Commands.CreateRoleAccess;

public class CreateRoleAccessCommandHandler(IRoleAccessRepository roleAccessRepository, ILogger<CreateRoleAccessCommandHandler> logger) : IRequestHandler<CreateRoleAccessCommand, Result>
{
    public async Task<Result> Handle(CreateRoleAccessCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на создание нового предоставления роли в базе данных.");
        var RoleAccess = new Domain.Entities.RoleAccess {
            UserId = request.UserId,
            RoleId = request.RoleId,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            GivenBy = request.GivenBy,
        };
        var result =  await roleAccessRepository.AddAsync(RoleAccess);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового предоставления роли в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}