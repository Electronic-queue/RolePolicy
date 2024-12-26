using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessById;

public class GetRoleAccessByIdQueryHandler(IRoleAccessRepository roleAccessRepository) : IRequestHandler<GetRoleAccessByIdQuery, Result<RoleAccess>>
{
    public async Task<Result<RoleAccess>> Handle(GetRoleAccessByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await roleAccessRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            return Result.Failure<RoleAccess>(record.Error);
        }
        return record;
    }
}
