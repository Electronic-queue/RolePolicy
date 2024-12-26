using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Resources.Commands.CreateResource;

public record CreateResourceCommand(
    string Name,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy) : IRequest<Result>;
