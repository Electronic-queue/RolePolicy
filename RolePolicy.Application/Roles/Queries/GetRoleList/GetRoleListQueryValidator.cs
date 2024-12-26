using FluentValidation;

namespace RolePolicy.Application.Roles.Queries.GetRoleList;

public class GetRoleListQueryValidator : AbstractValidator<GetRoleListQuery>
{
    public GetRoleListQueryValidator()
    {
    }
}
