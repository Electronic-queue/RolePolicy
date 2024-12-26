using FluentValidation;

namespace RolePolicy.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(100).WithMessage("FirstName не должно превышать 100 символов.")
            .NotEmpty().WithMessage("FirstName обязательно.");

        RuleFor(x => x.LastName)
            .MaximumLength(100).WithMessage("LastName не должно превышать 100 символов.")
            .NotEmpty().WithMessage("LastName обязательно.");

        RuleFor(x => x.Surname)
            .MaximumLength(100).WithMessage("Surname не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.Surname));

        RuleFor(x => x.Login)
            .MaximumLength(100).WithMessage("Login не должно превышать 100 символов.")
            .NotEmpty().WithMessage("Login обязательно.");

        RuleFor(x => x.PasswordHash)
            .MaximumLength(500).WithMessage("PasswordHash не должно превышать 500 символов.")
            .NotEmpty().WithMessage("PasswordHash обязательно.");

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy должен быть больше нуля.");
    }
}
