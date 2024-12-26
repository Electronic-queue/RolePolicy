using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<CreateRoleCommand, Result>
{
    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Domain.Entities.Role {
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await roleRepository.AddAsync(role);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}