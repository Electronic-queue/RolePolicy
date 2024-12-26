using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<DeleteRoleCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result =  await roleRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}