using FluentValidation;

namespace RolePolicy.Application.Users.Queries.GetUserList;

public class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
{
    public GetUserListQueryValidator()
    {
    }
}
