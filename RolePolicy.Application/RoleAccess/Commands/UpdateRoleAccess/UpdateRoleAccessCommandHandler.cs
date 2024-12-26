using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Commands.UpdateRoleAccess;

public class UpdateRoleAccessCommandHandler(IRoleAccessRepository roleAccessRepository) : IRequestHandler<UpdateRoleAccessCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleAccessCommand request, CancellationToken cancellationToken)
    {
        var result =  await roleAccessRepository.UpdateAsync(request.Id, request.UserId, request.RoleId, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}