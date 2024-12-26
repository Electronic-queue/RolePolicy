using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository UserRepository) : IRequestHandler<GetUserByIdQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await UserRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            return Result.Failure<User>(record.Error);
        }
        return record;
    }
}
