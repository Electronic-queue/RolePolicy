using FluentValidation;

namespace RolePolicy.Application.Actions.Queries.GetActionList;

public class GetActionListQueryValidator : AbstractValidator<GetActionListQuery>
{
    public GetActionListQueryValidator()
    {
    }
}
