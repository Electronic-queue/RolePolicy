using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Commands.CreateRoleAccess;

public class CreateRoleAccessCommandHandler(IRoleAccessRepository roleAccessRepository) : IRequestHandler<CreateRoleAccessCommand, Result>
{
    public async Task<Result> Handle(CreateRoleAccessCommand request, CancellationToken cancellationToken)
    {
        var RoleAccess = new Domain.Entities.RoleAccess {
            UserId = request.UserId,
            RoleId = request.RoleId,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            GivenBy = request.GivenBy,
        };
        var result =  await roleAccessRepository.AddAsync(RoleAccess);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}