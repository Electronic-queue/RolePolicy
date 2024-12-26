using FluentValidation;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessList;

public class GetRoleAccessListQueryValidator : AbstractValidator<GetRoleAccessListQuery>
{
    public GetRoleAccessListQueryValidator()
    {
    }
}
