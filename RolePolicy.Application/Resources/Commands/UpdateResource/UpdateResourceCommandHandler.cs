using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Commands.UpdateResource;

public class UpdateResourceCommandHandler(IResourceRepository resourceRepository, ILogger<UpdateResourceCommandHandler> logger) : IRequestHandler<UpdateResourceCommand, Result>
{
    public async Task<Result> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на обновление ресурса в базе данных.");
        var result =  await resourceRepository.UpdateAsync(request.Id, request.Name, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление ресурса в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}