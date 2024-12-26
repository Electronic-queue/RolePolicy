using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Queries.GetRoleList;

public class GetRoleListQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleListQuery, Result<List<Role>>>
{
    public async Task<Result<List<Role>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var records = await roleRepository.GetAllAsync();
        if (records.IsFailed)
        {
            return Result.Failure<List<Role>>(records.Error);
        }
        return records;
    }
}
