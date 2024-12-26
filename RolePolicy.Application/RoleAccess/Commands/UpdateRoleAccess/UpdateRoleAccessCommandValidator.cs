using FluentValidation;

namespace RolePolicy.Application.RoleAccesses.Commands.UpdateRoleAccess;

public class UpdateRoleAccessCommandValidator : AbstractValidator<UpdateRoleAccessCommand>
{
    public UpdateRoleAccessCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть больше нуля.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId должен быть больше нуля.");

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
