using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Resources.Queries.GetResourceList;

public record GetResourceListQuery() : IRequest<Result<List<Resource>>>;
