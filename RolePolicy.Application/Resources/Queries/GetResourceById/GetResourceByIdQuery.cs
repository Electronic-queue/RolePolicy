using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Resources.Queries.GetResourceById;

public record GetResourceByIdQuery(int Id) : IRequest<Result<Resource>>;
