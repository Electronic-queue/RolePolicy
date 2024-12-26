using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Queries.GetResourceList;

public class GetResourceListQueryHandler(IResourceRepository resourceRepository) : IRequestHandler<GetRoleAccessListQuery, Result<List<Resource>>>
{
    public async Task<Result<List<Resource>>> Handle(GetRoleAccessListQuery request, CancellationToken cancellationToken)
    {
        var records = await resourceRepository.GetAllAsync();
        if (records.IsFailed)
        {
            return Result.Failure<List<Resource>>(records.Error);
        }
        return records;
    }
}
