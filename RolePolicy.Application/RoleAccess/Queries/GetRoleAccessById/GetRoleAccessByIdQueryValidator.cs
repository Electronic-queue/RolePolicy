using FluentValidation;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessById;

public class GetRoleAccessByIdQueryValidator : AbstractValidator<GetRoleAccessByIdQuery>
{
    public GetRoleAccessByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
