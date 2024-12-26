using FluentValidation;

namespace RolePolicy.Application.RoleResourceActions.Commands.UpdateRoleResourceAction;

public class UpdateRoleResourceActionCommandValidator : AbstractValidator<UpdateRoleResourceActionCommand>
{
    public UpdateRoleResourceActionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId должен быть больше нуля.");

        RuleFor(x => x.ResourceId)
            .GreaterThan(0).WithMessage("ResourceId должен быть больше нуля.");

        RuleFor(x => x.ActionId)
            .GreaterThan(0).WithMessage("ActionId должен быть больше нуля.");

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
