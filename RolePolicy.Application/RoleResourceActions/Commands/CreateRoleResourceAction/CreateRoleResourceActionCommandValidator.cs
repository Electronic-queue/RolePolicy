using FluentValidation;

namespace RolePolicy.Application.RoleResourceActions.Commands.CreateRoleResourceAction;

public class CreateRoleResourceActionCommandValidator : AbstractValidator<CreateRoleResourceActionCommand>
{
    public CreateRoleResourceActionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId должен быть больше нуля.")
            .NotEmpty().WithMessage("RoleId обязательно.");

        RuleFor(x => x.ResourceId)
            .GreaterThan(0).WithMessage("ResourceId должен быть больше нуля.")
            .NotEmpty().WithMessage("ResourceId обязательно.");

        RuleFor(x => x.ActionId)
            .GreaterThan(0).WithMessage("ActionId должен быть больше нуля.")
            .NotEmpty().WithMessage("ActionId обязательно.");

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
            .NotEmpty().WithMessage("CeatedBy обязательно.");
    }
}
