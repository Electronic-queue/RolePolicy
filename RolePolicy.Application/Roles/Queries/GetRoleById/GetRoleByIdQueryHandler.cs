using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleByIdQuery, Result<Role>>
{
    public async Task<Result<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await roleRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            return Result.Failure<Role>(record.Error);
        }
        return record;
    }
}
