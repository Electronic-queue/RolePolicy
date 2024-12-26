using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Users.Queries.GetUserList;

public record GetUserListQuery() : IRequest<Result<List<User>>>;
