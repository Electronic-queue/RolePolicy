using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string? Surname,
    string Login,
    string PasswordHash,
    int? CreatedBy) : IRequest<Result>;
