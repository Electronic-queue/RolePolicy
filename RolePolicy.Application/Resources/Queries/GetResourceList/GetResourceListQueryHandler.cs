using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Queries.GetResourceList;

public class GetResourceListQueryHandler(IResourceRepository resourceRepository, ILogger<GetResourceListQueryHandler> logger) : IRequestHandler<GetResourceListQuery, Result<List<Resource>>>
{
    public async Task<Result<List<Resource>>> Handle(GetResourceListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка ресурсов из базы данных.");
        var records = await resourceRepository.GetAllAsync();
        if (records.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка ресурсов из базы данных.", records.Error.Code);
            return Result.Failure<List<Resource>>(records.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return records;
    }
}
