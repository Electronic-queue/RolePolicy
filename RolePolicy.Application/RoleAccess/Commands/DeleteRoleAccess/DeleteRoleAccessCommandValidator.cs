using FluentValidation;

namespace RolePolicy.Application.RoleAccesses.Commands.DeleteRoleAccess;

public class DeleteRoleAccessCommandValidator : AbstractValidator<DeleteRoleAccessCommand>
{
    public DeleteRoleAccessCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
