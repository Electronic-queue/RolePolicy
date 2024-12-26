using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Users.Queries.GetUserList;

public class GetUserListQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserListQuery, Result<List<User>>>
{
    public async Task<Result<List<User>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var records = await userRepository.GetAllAsync();
        if (records.IsFailed)
        {
            return Result.Failure<List<User>>(records.Error);
        }
        return records;
    }
}
