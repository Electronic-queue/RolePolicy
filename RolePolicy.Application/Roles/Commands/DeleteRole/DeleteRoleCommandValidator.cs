using FluentValidation;
using RolePolicy.Application.Roles.Commands.DeleteRole;

namespace RolePolicy.Application.Roles.Commands.DeleteRolee;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
