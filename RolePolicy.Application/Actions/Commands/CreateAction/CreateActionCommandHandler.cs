using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Utils;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Actions.Commands.CreateAction;

public class CreateActionCommandHandler(IActionRepository actionRepository, ILogger<CreateActionCommandHandler> logger) : IRequestHandler<CreateActionCommand, Result>
{
    public async Task<Result> Handle(CreateActionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на создание нового действия в базе данных.");
        var action = new Domain.Entities.Action {
            Name = request.Name,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = Date.GetDate(),
            CreatedBy = request.CreatedBy,
        };
        var result =  await actionRepository.AddAsync(action);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового действия в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}