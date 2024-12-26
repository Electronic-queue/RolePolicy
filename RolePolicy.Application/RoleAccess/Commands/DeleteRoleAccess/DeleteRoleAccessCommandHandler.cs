using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Commands.DeleteRoleAccess;

public class DeleteRoleAccessCommandHandler(IRoleAccessRepository roleAccessRepository) : IRequestHandler<DeleteRoleAccessCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleAccessCommand request, CancellationToken cancellationToken)
    {
        var result =  await roleAccessRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}