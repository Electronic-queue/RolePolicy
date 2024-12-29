using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class RoleAccessRepository(RolePolicyDbContext dbContext, ILogger<RoleAccessRepository> logger) : IRoleAccessRepository
{
    public async Task<Result> AddAsync(RoleAccess roleAccess)
    {
        try
        {
            logger.LogInformation("Добавление нового предоставления роли в базу данных.");
            await dbContext.AddAsync(roleAccess);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Предоставление {TargetUserId} роли {TargetRoleId} добавлено в базу данных.", roleAccess.UserId, roleAccess.RoleId);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении предоставления {TargetUserId} роли {TargetRoleId} в базу данных.", roleAccess.UserId, roleAccess.RoleId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении предоставления {roleAccess.UserId} роли {roleAccess.RoleId} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление предоставления роли из базы данных.");
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление роли с id {TargetRoleAccessId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Предоставление роли с id {TargetRoleAccessId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении предоставления роли с id {TargetRoleAccessId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении предоставления роли с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<RoleAccess>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка предоставлений роли из базы данных.");
            var entities = await dbContext.RoleAccesses.ToListAsync();
            logger.LogInformation("Полный список предоставлений роли получен из базы данных.");
            return Result.Success(entities);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка предоставлений роли из базы данных.");
            return Result.Failure<List<RoleAccess>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка предоставлений роли из базы данных."));
        }
    }

    public async Task<Result<RoleAccess>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение предоставления роли из базы данных."); 
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Предоставление роли с id {TargetRoleAccessId} получено из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Предоставление роли с id {TargetRoleAccessId} не найдено в базе данных.", id);
            return Result.Failure<RoleAccess>(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении предоставления роли с id {TargetRoleAccessId} из базы данных.", id);
            return Result.Failure<RoleAccess>(new Error(Errors.InternalServerError, $"Ошибка при получении предоставления роли с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, int? userId = null, int? roleId = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление предоставления роли в базе данных.");
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                entity.UserId = userId ?? entity.UserId;
                entity.RoleId = roleId ?? entity.RoleId;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление роли с id {TargetRoleAccessId} обновлено в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Предоставление роли с id {TargetRoleAccessId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при обновлении предоставления роли с id {TargetRoleAccessId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении предоставления роли с id {id} в базе данных."));
        }
    }
}
