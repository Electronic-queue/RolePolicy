using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class ResourceRepository(RolePolicyDbContext dbContext, ILogger<ResourceRepository> logger) : IResourceRepository
{
    public async Task<Result> AddAsync(Resource resource)
    {
        try
        {
            logger.LogInformation("Добавление нового ресурса в базу данных.");
            await dbContext.AddAsync(resource);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Ресурс {TargetName} добавлен в базу данных.", resource.Name);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении ресурса {TargetName} в базу данных.", resource.Name);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении нового ресурса {resource.Name} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление ресурса из базы данных.");
            var entity = await dbContext.Resources.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Ресурс с id {TargetResourceId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Ресурс с id {TargetResourceId} не найден в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Ресурс с id {id} не найден в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении ресурса с id {TargetResourceId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении ресурса с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Resource>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка ресурсов из базы данных.");
            var entities = await dbContext.Resources.ToListAsync();
            logger.LogInformation("Полный список ресурсов получен из базы данных.");
            return Result.Success(entities);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка ресурсов из базы данных.");
            return Result.Failure<List<Resource>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка ресурсов из базы данных."));
        }
    }

    public async Task<Result<Resource>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение ресурса из базы данных.");
            var entity = await dbContext.Resources.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Ресурс с id {TargetResourceId} получен из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Ресурс с id {TargetResourceId} не найден в базе данных.", id);
            return Result.Failure<Resource>(new Error(Errors.NotFound, $"Ресурс с id {id} не найден в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении ресурса с id {TargetResourceId} из базы данных.", id);
            return Result.Failure<Resource>(new Error(Errors.InternalServerError, $"Ошибка при получении ресурса с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? name = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление ресурса в базе данных.");
            var entity = await dbContext.Resources.FindAsync(id);
            if (entity != null)
            {
                entity.Name = name ?? entity.Name;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Ресурс с id {TargetResourceId} обновлен в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Ресурс с id {TargetResourceId} не найден в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Ресурс с id {id} не найден в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при обновлении ресурса с id {TargetResourceId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении ресурса с id {id} в базе данных."));
        }
    }
}
