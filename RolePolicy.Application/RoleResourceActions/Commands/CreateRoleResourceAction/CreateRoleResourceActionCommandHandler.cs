using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Commands.CreateRoleResourceAction;

public class CreateRoleResourceActionCommandHandler(IRoleResourceActionRepository roleResourceActionRepository, ILogger<CreateRoleResourceActionCommandHandler> logger) : IRequestHandler<CreateRoleResourceActionCommand, Result>
{
    public async Task<Result> Handle(CreateRoleResourceActionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на создание новой связи роли и действия над ресурсом в базе данных.");
        var RoleResourceAction = new Domain.Entities.RoleResourceAction {
            RoleId = request.RoleId,
            ResourceId = request.ResourceId,
            ActionId = request.ActionId,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await roleResourceActionRepository.AddAsync(RoleResourceAction);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание новой связи роли и действия над ресурсом в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}