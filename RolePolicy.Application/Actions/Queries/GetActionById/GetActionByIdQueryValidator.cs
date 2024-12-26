using FluentValidation;

namespace RolePolicy.Application.Actions.Queries.GetActionById;

public class GetActionByIdQueryValidator : AbstractValidator<GetActionByIdQuery>
{
    public GetActionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
