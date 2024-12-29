using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Commands.UpdateRoleResourceAction;

public class UpdateRoleResourceActionCommandHandler(IRoleResourceActionRepository roleResourceActionRepository, ILogger<UpdateRoleResourceActionCommandHandler> logger) : IRequestHandler<UpdateRoleResourceActionCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleResourceActionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на обновление связи роли и действия над ресурсом в базе данных.");
        var result =  await roleResourceActionRepository.UpdateAsync(request.Id, request.RoleId, request.ResourceId, request.ActionId, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление связи роли и действия над ресурсом в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}