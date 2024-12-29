using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;
using System.Data;


namespace RolePolicy.Persistence.Repository;

public class RoleResourceActionRepository(RolePolicyDbContext dbContext, ILogger<RoleResourceActionRepository> logger) : IRoleResourceActionRepository
{
    public async Task<Result> AddAsync(RoleResourceAction roleResourceAction)
    {
        try
        {
            logger.LogInformation("Добавление новой связи роли и действия над ресурсом в базу данных.");
            await dbContext.AddAsync(roleResourceAction);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Связь роли {TargetRoleId} и действия {TargetActionId} над ресурсом {TargetResourceId} добавлена в базу данных.", roleResourceAction.RoleId, roleResourceAction.ActionId, roleResourceAction.ResourceId);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении связи роли {TargetRoleId} и действия {TargetActionId} над ресурсом {TargetResourceId} в базу данных.", roleResourceAction.RoleId, roleResourceAction.ActionId, roleResourceAction.ResourceId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении связи роли {roleResourceAction.RoleId} и действия {roleResourceAction.ActionId} над ресурсом {roleResourceAction.ResourceId} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление связи роли и действия над ресурсом из базы данных.");
            var entity = await dbContext.RoleResourceActions.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Связь роли и действия над ресурсом с id {TargetRoleResourceActionId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Связь роли и действия над ресурсом с id {TargetRoleResourceActionId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Связь роли и действия над ресурсом с id {id} не найдена в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении связи роли и действия над ресурсом с id {TargetRoleResourceActionId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении связи роли и действия над ресурсом с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<RoleResourceAction>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка связей роли и действия над ресурсом из базы данных.");
            var entities = await dbContext.RoleResourceActions.ToListAsync();
            logger.LogInformation("Полный список связей роли и действия над ресурсом получен из базы данных.");
            return Result.Success(entities);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка связей роли и действия над ресурсом из базы данных.");
            return Result.Failure<List<RoleResourceAction>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка связей роли и действия над ресурсом из базы данных."));
        }
    }

    public async Task<Result<RoleResourceAction>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение связи роли и действия над ресурсом из базы данных.");
            var entity = await dbContext.RoleResourceActions.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Связь роли и действия над ресурсом с id {TargetRoleResourceActionId} получена из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Связь роли и действия над ресурсом с id {TargetRoleResourceActionId} не найдена в базе данных.", id);
            return Result.Failure<RoleResourceAction>(new Error(Errors.NotFound, $"Связь роли и действия над ресурсом с id {id} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении связи роли и действия над ресурсом с id {TargetRoleResourceActionId} из базы данных.", id);
            return Result.Failure<RoleResourceAction>(new Error(Errors.InternalServerError, $"Ошибка при получении связи роли и действия над ресурсом с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, int? roleId = null, int? resourceId = null, int? actionId = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление связи роли и действия над ресурсом в базе данных.");
            var entity = await dbContext.RoleResourceActions.FindAsync(id);
            if (entity != null)
            {
                entity.RoleId = roleId ?? entity.RoleId;
                entity.ResourceId = resourceId ?? entity.ResourceId;
                entity.ActionId = actionId ?? entity.ActionId;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Связь роли и действия над ресурсом с id {TargetRoleResourceActionId} обновлена в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Связь роли и действия над ресурсом с id {TargetRoleResourceActionId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Связь роли и действия над ресурсом с id {id} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении связи роли и действия над ресурсом с id {TargetRoleResourceActionId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении связи роли и действия над ресурсом с id {id} в базе данных."));
        }
    }
}
