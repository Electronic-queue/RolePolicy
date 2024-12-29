using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Commands.DeleteRoleResourceAction;

public class DeleteRoleResourceActionCommandHandler(IRoleResourceActionRepository roleResourceActionRepository, ILogger<DeleteRoleResourceActionCommandHandler> logger) : IRequestHandler<DeleteRoleResourceActionCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleResourceActionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на удаление связи роли и действия над ресурсом из базы данных.");
        var result =  await roleResourceActionRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление связи роли и действия над ресурсом из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}