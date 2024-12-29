using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Commands.DeleteResource;

public class DeleteResourceCommandHandler(IResourceRepository resourceRepository, ILogger<DeleteResourceCommandHandler> logger) : IRequestHandler<DeleteResourceCommand, Result>
{
    public async Task<Result> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на удаление ресурса из базы данных.");
        var result =  await resourceRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление ресурса из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}