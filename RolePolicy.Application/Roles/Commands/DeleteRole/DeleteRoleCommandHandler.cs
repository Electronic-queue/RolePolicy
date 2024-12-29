using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler(IRoleRepository roleRepository, ILogger<DeleteRoleCommandHandler> logger) : IRequestHandler<DeleteRoleCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на удаление роли из базы данных.");
        var result =  await roleRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление роли из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}