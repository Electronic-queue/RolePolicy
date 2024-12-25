using KDS.Primitives.FluentResult;
using MediatR;

namespace RolePolicy.Application.Actions.Commands.CreateAction;

public record CreateActionCommand(
    string Name = "",
    string? DescriptionRu = null,
    string? DescriptionKk = null,
    string? DescriptionEn = null,
    int? CreatedBy = null) : IRequest<Result>;
