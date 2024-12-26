using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionList;

public class GetRoleResourceActionListQueryHandler(IRoleResourceActionRepository roleResourceActionRepository) : IRequestHandler<GetRoleResourceActionListQuery, Result<List<RoleResourceAction>>>
{
    public async Task<Result<List<RoleResourceAction>>> Handle(GetRoleResourceActionListQuery request, CancellationToken cancellationToken)
    {
        var records = await roleResourceActionRepository.GetAllAsync();
        if (records.IsFailed)
        {
            return Result.Failure<List<RoleResourceAction>>(records.Error);
        }
        return records;
    }
}
