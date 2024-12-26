using FluentValidation;

namespace RolePolicy.Application.Actions.Commands.DeleteAction;

public class DeleteActionCommandValidator : AbstractValidator<DeleteActionCommand>
{
    public DeleteActionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
