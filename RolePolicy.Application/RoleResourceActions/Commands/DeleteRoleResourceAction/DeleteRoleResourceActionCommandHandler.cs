using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Commands.DeleteRoleResourceAction;

public class DeleteRoleResourceActionCommandHandler(IRoleResourceActionRepository roleResourceActionRepository) : IRequestHandler<DeleteRoleResourceActionCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleResourceActionCommand request, CancellationToken cancellationToken)
    {
        var result =  await roleResourceActionRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}