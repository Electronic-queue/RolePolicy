using FluentValidation;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionById;

public class GetRoleResourceActionByIdQueryValidator : AbstractValidator<GetRoleResourceActionByIdQuery>
{
    public GetRoleResourceActionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
