using FluentValidation;

namespace RolePolicy.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.NameRu)
            .MaximumLength(100).WithMessage("NameRu не должно превышать 100 символов.")
            .NotEmpty().WithMessage("NameRu обязательно.");

        RuleFor(x => x.NameKk)
            .MaximumLength(100).WithMessage("NameKk не должно превышать 100 символов.")
            .NotEmpty().WithMessage("NameKk обязательно.");

        RuleFor(x => x.NameEn)
            .MaximumLength(100).WithMessage("NameEn не должно превышать 100 символов.")
            .NotEmpty().WithMessage("NameEn обязательно.");

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionRu));

        RuleFor(x => x.DescriptionKk)
            .MaximumLength(500).WithMessage("DescriptionKk не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionKk));

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(500).WithMessage("DescriptionEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionEn));

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy должен быть больше нуля.")
            .NotEmpty().WithMessage("CreatedBy обязательно.");
    }
}
