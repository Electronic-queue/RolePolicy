using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler(IRoleRepository roleRepository, ILogger<UpdateRoleCommandHandler> logger) : IRequestHandler<UpdateRoleCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на обновление роли в базе данных.");
        var result =  await roleRepository.UpdateAsync(request.Id, request.NameRu, request.NameKk, request.NameEn, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление роли в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}