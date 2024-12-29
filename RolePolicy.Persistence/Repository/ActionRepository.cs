using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;
using Action = RolePolicy.Domain.Entities.Action;


namespace RolePolicy.Persistence.Repository;

public class ActionRepository(RolePolicyDbContext dbContext, ILogger<ActionRepository> logger) : IActionRepository
{
    public async Task<Result> AddAsync(Action action)
    {
        try
        {
            logger.LogInformation("Добавление нового действия в базу данных.");
            await dbContext.AddAsync(action);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Действие {TargetName} добавлено в базу данных.", action.Name);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении действия {TargetActionName} в базу данных.", action.Name);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении нового действия {action.Name} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление действия из базы данных.");
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Действие с id {TargetActionId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Действие с id {TargetActionId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении действия с id {TargetActionId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении действия с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Action>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка действий из базы данных.");
            var entities = await dbContext.Actions.ToListAsync();
            logger.LogInformation("Полный список действий получен из базы данных.");
            return Result.Success(entities);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка действий из базы данных.");
            return Result.Failure<List<Action>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка действий из базы данных."));
        }
    }

    public async Task<Result<Action>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение действия из базы данных.");
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Действие с id {TargetActionId} получено из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Действие с id {TargetActionId} не найдено в базе данных.", id);
            return Result.Failure<Action>(new Error(Errors.NotFound, $"Действие с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении действия с id {TargetActionId} из базы данных.", id);
            return Result.Failure<Action>(new Error(Errors.InternalServerError, $"Ошибка при получении действия с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? name = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление действия в базе данных.");
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                entity.Name = name ?? entity.Name;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Действие с id {TargetActionId} обновлено в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Действие с id {TargetActionId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {id} не найдено в базе данных."));
        }
        catch( Exception)
        {
            logger.LogError("Ошибка при обновлении действия с id {TargetActionId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении действия с id {id} в базе данных."));
        }
    }
}
