using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository UserRepository) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result =  await UserRepository.UpdateAsync(request.Id, request.FirstName, request.LastName, request.Surname, request.Login, request.PasswordHash, request.IsDeleted);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}