using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Resources.Commands.DeleteResource;

public record DeleteResourceCommand(int Id) : IRequest<Result>;
