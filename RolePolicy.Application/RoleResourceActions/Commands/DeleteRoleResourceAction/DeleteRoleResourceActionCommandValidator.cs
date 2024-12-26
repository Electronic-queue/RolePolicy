using FluentValidation;

namespace RolePolicy.Application.RoleResourceActions.Commands.DeleteRoleResourceAction;

public class DeleteRoleResourceActionCommandValidator : AbstractValidator<DeleteRoleResourceActionCommand>
{
    public DeleteRoleResourceActionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
