using FluentValidation;

namespace RolePolicy.Application.Resources.Queries.GetResourceList;

public class GetResourceListQueryValidator : AbstractValidator<GetRoleAccessListQuery>
{
    public GetResourceListQueryValidator()
    {
    }
}
