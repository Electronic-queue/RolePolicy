using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleResourceActions.Commands.CreateRoleResourceAction;

public class CreateRoleResourceActionCommandHandler(IRoleResourceActionRepository roleResourceActionRepository) : IRequestHandler<CreateRoleResourceActionCommand, Result>
{
    public async Task<Result> Handle(CreateRoleResourceActionCommand request, CancellationToken cancellationToken)
    {
        var RoleResourceAction = new Domain.Entities.RoleResourceAction {
            RoleId = request.RoleId,
            ResourceId = request.ResourceId,
            ActionId = request.ActionId,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await roleResourceActionRepository.AddAsync(RoleResourceAction);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}