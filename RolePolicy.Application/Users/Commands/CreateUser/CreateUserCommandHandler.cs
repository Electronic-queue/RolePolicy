using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository UserRepository) : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Surname = request.Surname,
            Login = request.Login,
            PasswordHash = request.PasswordHash,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await UserRepository.AddAsync(user);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}