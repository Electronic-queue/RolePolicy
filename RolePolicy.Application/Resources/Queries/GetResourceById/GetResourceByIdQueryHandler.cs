using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Queries.GetResourceById;

public class GetResourceByIdQueryHandler(IResourceRepository ResourceRepository, ILogger<GetResourceByIdQueryHandler> logger) : IRequestHandler<GetResourceByIdQuery, Result<Resource>>
{
    public async Task<Result<Resource>> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение ресурса из базы данных.");
        var record = await ResourceRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение ресурса из базы данных.", record.Error.Code);
            return Result.Failure<Resource>(record.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return record;
    }
}
