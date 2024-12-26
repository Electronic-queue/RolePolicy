using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Queries.GetResourceList;

public class GetResourceListQueryHandler(IResourceRepository resourceRepository) : IRequestHandler<GetResourceListQuery, Result<List<Resource>>>
{
    public async Task<Result<List<Resource>>> Handle(GetResourceListQuery request, CancellationToken cancellationToken)
    {
        var records = await resourceRepository.GetAllAsync();
        if (records.IsFailed)
        {
            return Result.Failure<List<Resource>>(records.Error);
        }
        return records;
    }
}
