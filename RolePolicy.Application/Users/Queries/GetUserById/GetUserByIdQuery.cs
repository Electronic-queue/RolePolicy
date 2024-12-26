using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Result<User>>;
