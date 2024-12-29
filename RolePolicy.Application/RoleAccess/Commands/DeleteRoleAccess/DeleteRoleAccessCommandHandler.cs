using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.RoleAccesses.Commands.DeleteRoleAccess;

public class DeleteRoleAccessCommandHandler(IRoleAccessRepository roleAccessRepository, ILogger<DeleteRoleAccessCommandHandler> logger) : IRequestHandler<DeleteRoleAccessCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleAccessCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на удаление предоставления роли из базы данных.");
        var result =  await roleAccessRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление предоставления роли из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}