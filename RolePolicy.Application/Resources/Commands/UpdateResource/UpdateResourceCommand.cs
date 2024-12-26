using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Resources.Commands.UpdateResource;

public record UpdateResourceCommand(
    int Id,
    string? Name = null,
    string? DescriptionRu = null,
    string? DescriptionKk = null,
    string? DescriptionEn = null) : IRequest<Result>;
