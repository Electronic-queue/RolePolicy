using FluentValidation;

namespace RolePolicy.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");

        RuleFor(x => x.FirstName)
            .MaximumLength(100).WithMessage("FirstName не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(100).WithMessage("LastName не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.LastName));

        RuleFor(x => x.Surname)
            .MaximumLength(100).WithMessage("Surname не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.Surname));

        RuleFor(x => x.Login)
            .MaximumLength(100).WithMessage("Login не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.Login));

        RuleFor(x => x.PasswordHash)
            .MaximumLength(500).WithMessage("PasswordHash не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.PasswordHash));
    }
}
