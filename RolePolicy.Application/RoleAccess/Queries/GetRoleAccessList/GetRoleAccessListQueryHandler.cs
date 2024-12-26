using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessList;

public class GetRoleAccessListQueryHandler(IRoleAccessRepository roleAccessRepository) : IRequestHandler<GetRoleAccessListQuery, Result<List<RoleAccess>>>
{
    public async Task<Result<List<RoleAccess>>> Handle(GetRoleAccessListQuery request, CancellationToken cancellationToken)
    {
        var records = await roleAccessRepository.GetAllAsync();
        if (records.IsFailed)
        {
            return Result.Failure<List<RoleAccess>>(records.Error);
        }
        return records;
    }
}
