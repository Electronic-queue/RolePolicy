using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessById;

public record GetRoleAccessByIdQuery(int Id) : IRequest<Result<RoleAccess>>;
