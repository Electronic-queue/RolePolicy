using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Actions.Commands.DeleteAction;

public record DeleteActionCommand(int Id) : IRequest<Result>;
