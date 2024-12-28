using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Actions.Commands.CreateAction;

public record CreateActionCommand(
    string Name,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int CreatedBy) : IRequest<Result>;
