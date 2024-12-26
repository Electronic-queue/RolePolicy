using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.RoleAccesses.Commands.CreateRoleAccess;

public record CreateRoleAccessCommand(
    int UserId,
    int RoleId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int GivenBy) : IRequest<Result>;
