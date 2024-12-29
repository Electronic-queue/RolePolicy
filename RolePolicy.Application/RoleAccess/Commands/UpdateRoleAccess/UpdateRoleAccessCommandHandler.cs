using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Commands.UpdateRoleAccess;

public class UpdateRoleAccessCommandHandler(IRoleAccessRepository roleAccessRepository, ILogger<UpdateRoleAccessCommandHandler> logger) : IRequestHandler<UpdateRoleAccessCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleAccessCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на обновление предоставления роли в базе данных.");
        var result =  await roleAccessRepository.UpdateAsync(request.Id, request.UserId, request.RoleId, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление предоставления роли в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}