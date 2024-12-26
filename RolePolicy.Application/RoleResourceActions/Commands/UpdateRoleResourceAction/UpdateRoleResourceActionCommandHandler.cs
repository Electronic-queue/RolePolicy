using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Commands.UpdateRoleResourceAction;

public class UpdateRoleResourceActionCommandHandler(IRoleResourceActionRepository roleResourceActionRepository) : IRequestHandler<UpdateRoleResourceActionCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleResourceActionCommand request, CancellationToken cancellationToken)
    {
        var result =  await roleResourceActionRepository.UpdateAsync(request.Id, request.RoleId, request.ResourceId, request.ActionId, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}