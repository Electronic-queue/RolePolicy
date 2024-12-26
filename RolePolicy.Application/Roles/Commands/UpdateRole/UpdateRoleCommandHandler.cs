using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<UpdateRoleCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var result =  await roleRepository.UpdateAsync(request.Id, request.NameRu, request.NameKk, request.NameEn, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}