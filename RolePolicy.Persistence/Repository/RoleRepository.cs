using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class RoleRepository(RolePolicyDbContext dbContext, ILogger<RoleRepository> logger) : IRoleRepository
{
    public async Task<Result> AddAsync(Role role)
    {
        try
        {
            logger.LogInformation("Добавление новой роли в базу данных.");
            await dbContext.AddAsync(role);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Роль {TargetNameRu}({TargetNameKk}, {TargetNameEn}) добавлена в базу данных.", role.NameRu, role.NameKk, role.NameEn);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении роли {TargetNameRu}({TargetNameKk}, {TargetNameEn}) в базу данных.", role.NameRu, role.NameKk, role.NameEn);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении роли {role.NameRu}({role.NameKk}, {role.NameEn}) в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление роли из базы данных.");
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Роль с id {TargetRoleId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Роль с id {TargetRoleId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Роль с id {id} не найдена в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении роли с id {TargetRoleId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении роли с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Role>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка ролей из базы данных.");
            var entities = await dbContext.Roles.ToListAsync();
            logger.LogInformation("Полный список ролей получен из базы данных.");
            return Result.Success(entities);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка ролей из базы данных.");
            return Result.Failure<List<Role>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка ролей из базы данных."));
        }
    }

    public async Task<Result<Role>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение роли из базы данных.");
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Роль с id {TargetRoleId} получена из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Роль с id {TargetRoleId} не найдена в базе данных.", id);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Роль с id {id} не найдена в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении роли с id {TargetRoleId} из базы данных.", id);
            return Result.Failure<Role>(new Error(Errors.InternalServerError, $"Ошибка при получении роли с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? nameRu = null, string? nameKk = null, string? nameEn = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление роли в базе данных.");
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity != null)
            {
                entity.NameRu = nameRu ?? entity.NameRu;
                entity.NameKk = nameKk ?? entity.NameKk;
                entity.NameEn = nameEn ?? entity.NameEn;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Роль с id {TargetRoleId} обновлена в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Роль с id {TargetRoleId} не найдена в базе данных.", id);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Роль с id {id} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении роли с id {TargetRoleId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении роли с id {id} в базе данных."));
        }
    }
}
