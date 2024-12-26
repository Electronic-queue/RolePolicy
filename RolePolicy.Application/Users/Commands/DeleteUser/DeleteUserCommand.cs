using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<Result>;
