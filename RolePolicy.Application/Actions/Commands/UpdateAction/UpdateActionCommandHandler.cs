using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Actions.Commands.UpdateAction;

public class UpdateActionCommandHandler(IActionRepository actionRepository) : IRequestHandler<UpdateActionCommand, Result>
{
    public async Task<Result> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
    {
        var result =  await actionRepository.UpdateAsync(request.Id, request.Name, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            return Result.Failure(new Error(Errors.BadRequest, $"Ошибка при обновлении сущности с id {request.Id} в таблице Actions."));
        }
        return Result.Success();
    }
}