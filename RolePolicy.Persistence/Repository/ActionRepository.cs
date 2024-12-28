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
            logger.LogInformation("Добавление нового действия {ActionName} в базу данных.", action.Name);
            await dbContext.AddAsync(action);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Действие {ActionName} добавлено в базу данных.", action.Name);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError("Ошибка при добавлении действия {ActionName} в базу данных.", action.Name);
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при добавлении нового действия в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление действия с id {ActionId} из базы данных.", id);
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Действие с id {ActionId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Действие с id {ActionId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {id} не найдено в базе данных."));
        }
        catch(Exception ex)
        {
            logger.LogError("Ошибка при удалении действия с id {ActionId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при удалении действия из базы данных."));
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
        catch(Exception ex)
        {
            logger.LogError("Ошибка при получении полного списка действий из базы данных.");
            return Result.Failure<List<Action>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка действий из базы данных."));
        }
    }

    public async Task<Result<Action>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение действия с id {ActionId} из базы данных.", id);
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Действие с id {ActionId} получено из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Действие с id {ActionId} не найдено в базе данных.", id);
            return Result.Failure<Action>(new Error(Errors.NotFound, $"Действие с id {id} не найдено в базе данных."));
        }
        catch(Exception ex)
        {
            logger.LogError("Ошибка при получении действия с id {ActionId} из базы данных.", id);
            return Result.Failure<Action>(new Error(Errors.InternalServerError, "Ошибка при получении действия из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? name = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление действия с id {ActionId} из базы данных.", id);
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                entity.Name = name ?? entity.Name;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Действие с id {ActionId} обновлено в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Действие с id {ActionId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {id} не найдено в базе данных."));
        }
        catch( Exception ex )
        {
            logger.LogError("Ошибка при обновлении действия с id {ActionId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при обновлении действия в базе данных."));
        }
    }
}
