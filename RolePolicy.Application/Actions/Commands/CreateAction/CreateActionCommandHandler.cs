using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Actions.Commands.CreateAction;

public class CreateActionCommandHandler(IActionRepository actionRepository) : IRequestHandler<CreateActionCommand, Result>
{
    public async Task<Result> Handle(CreateActionCommand request, CancellationToken cancellationToken)
    {
        var action = new Domain.Entities.Action {
            Name = request.Name,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await actionRepository.AddAsync(action);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}