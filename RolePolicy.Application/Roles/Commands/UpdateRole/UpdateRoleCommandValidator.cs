using FluentValidation;

namespace RolePolicy.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");

        RuleFor(x => x.NameRu)
            .MaximumLength(100).WithMessage("NameRu не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.NameRu));

        RuleFor(x => x.NameKk)
            .MaximumLength(100).WithMessage("NameKk не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.NameKk));

        RuleFor(x => x.NameEn)
            .MaximumLength(100).WithMessage("NameEn не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.NameEn));

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionRu));

        RuleFor(x => x.DescriptionKk)
            .MaximumLength(500).WithMessage("DescriptionKk не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionKk));

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(500).WithMessage("DescriptionEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionEn));
    }
}
