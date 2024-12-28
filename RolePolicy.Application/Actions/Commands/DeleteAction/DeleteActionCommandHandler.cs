using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Actions.Commands.DeleteAction;

public class DeleteActionCommandHandler(IActionRepository actionRepository, ILogger<DeleteActionCommandHandler> logger) : IRequestHandler<DeleteActionCommand, Result>
{
    public async Task<Result> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на удаление действия из базы данных.");
        var result =  await actionRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление действия.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}