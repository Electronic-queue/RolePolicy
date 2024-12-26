using FluentValidation;

namespace RolePolicy.Application.RoleAccesses.Commands.CreateRoleAccess;

public class CreateRoleAccessCommandValidator : AbstractValidator<CreateRoleAccessCommand>
{
    public CreateRoleAccessCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserId обязательно.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId должен быть больше нуля.")
            .NotEmpty().WithMessage("RoleId обязательно.");

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionRu));

        RuleFor(x => x.DescriptionKk)
            .MaximumLength(500).WithMessage("DescriptionKk не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionKk));

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(500).WithMessage("DescriptionEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionEn));

        RuleFor(x => x.GivenBy)
            .GreaterThan(0).WithMessage("GivenBy должен быть больше нуля.")
            .NotEmpty().WithMessage("GivenBy обязательно.");
    }
}
