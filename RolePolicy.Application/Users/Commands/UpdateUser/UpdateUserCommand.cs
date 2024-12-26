using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string? FirstName = null,
    string? LastName = null,
    string? Surname = null,
    string? Login = null,
    string? PasswordHash = null,
    bool? IsDeleted = null) : IRequest<Result>;