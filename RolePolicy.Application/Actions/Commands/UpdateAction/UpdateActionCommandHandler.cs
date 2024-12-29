using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Actions.Commands.UpdateAction;

public class UpdateActionCommandHandler(IActionRepository actionRepository, ILogger<UpdateActionCommandHandler> logger) : IRequestHandler<UpdateActionCommand, Result>
{
    public async Task<Result> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на обновление действия в базе данных.");
        var result =  await actionRepository.UpdateAsync(request.Id, request.Name, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление действия в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}