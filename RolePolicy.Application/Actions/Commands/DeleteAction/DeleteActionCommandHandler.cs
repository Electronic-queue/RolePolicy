﻿using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Actions.Commands.DeleteAction;

public class DeleteActionCommandHandler(IActionRepository actionRepository) : IRequestHandler<DeleteActionCommand, Result>
{
    public async Task<Result> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
    {
        var result =  await actionRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            return Result.Failure(new Error(Errors.BadRequest, $"Ошибка при удалении сущности с id {request.Id} в таблице Actions."));
        }
        return Result.Success();
    }
}