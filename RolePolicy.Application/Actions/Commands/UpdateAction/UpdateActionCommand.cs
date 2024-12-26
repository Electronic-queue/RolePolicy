using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Actions.Commands.UpdateAction;

public record UpdateActionCommand(
    int Id,
    string? Name = null,
    string? DescriptionRu = null,
    string? DescriptionKk = null,
    string? DescriptionEn = null) : IRequest<Result>;
