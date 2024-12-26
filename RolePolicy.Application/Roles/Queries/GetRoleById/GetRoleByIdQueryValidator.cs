using FluentValidation;

namespace RolePolicy.Application.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
