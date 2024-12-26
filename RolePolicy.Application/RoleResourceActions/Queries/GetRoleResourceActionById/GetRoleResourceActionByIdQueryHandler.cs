using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionById;

public class GetRoleResourceActionByIdQueryHandler(IRoleResourceActionRepository roleResourceActionRepository) : IRequestHandler<GetRoleResourceActionByIdQuery, Result<RoleResourceAction>>
{
    public async Task<Result<RoleResourceAction>> Handle(GetRoleResourceActionByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await roleResourceActionRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            return Result.Failure<RoleResourceAction>(record.Error);
        }
        return record;
    }
}
