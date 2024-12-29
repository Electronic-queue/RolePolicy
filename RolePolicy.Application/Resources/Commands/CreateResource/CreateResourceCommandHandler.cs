using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Commands.CreateResource;

public class CreateResourceCommandHandler(IResourceRepository resourceRepository, ILogger<CreateResourceCommandHandler> logger) : IRequestHandler<CreateResourceCommand, Result>
{
    public async Task<Result> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на создание нового ресурса в базе данных.");
        var resource = new Domain.Entities.Resource {
            Name = request.Name,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await resourceRepository.AddAsync(resource);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового ресурса в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}