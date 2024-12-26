using FluentValidation;

namespace RolePolicy.Application.Resources.Queries.GetResourceById;

public class GetResourceByIdQueryValidator : AbstractValidator<GetResourceByIdQuery>
{
    public GetResourceByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
