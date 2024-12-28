using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;
using System;
using System.IO;
using static System.Collections.Specialized.BitVector32;


namespace RolePolicy.Persistence.Repository;

public class RoleAccessRepository(RolePolicyDbContext dbContext, ILogger<RoleAccessRepository> logger) : IRoleAccessRepository
{
    public async Task<Result> AddAsync(RoleAccess roleAccess)
    {
        try
        {
            logger.LogInformation("Добавление нового предоставления {UserId} роли {RoleId} в базу данных.", roleAccess.UserId, roleAccess.RoleId);
            await dbContext.AddAsync(roleAccess);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Предоставление {UserId} роли {RoleId} добавлено в базу данных.", roleAccess.UserId, roleAccess.RoleId);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError("Ошибка при добавлении предоставления {UserId} роли {RoleId} в базу данных.", roleAccess.UserId, roleAccess.RoleId);
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при добавлении предоставления роли в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление предоставления роли с id {RoleAccessId} в базу данных.", id);
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление роли с id {RoleAccessId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Предоставление роли с id {RoleAccessId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено."));
        }
        catch(Exception ex)
        {
            logger.LogError("Ошибка при удалении предоставления роли с id {RoleAccessId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при удалении предоставления роли из базы данных."));
        }
    }

    public async Task<Result<List<RoleAccess>>> GetAllAsync()
    {
        try
        {
            var entities = await dbContext.RoleAccesses.ToListAsync();
            return Result.Success(entities);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<RoleAccess>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка предоставлений роли из базы данных."));
        }
    }

    public async Task<Result<RoleAccess>> GetById(int id)
    {
        try
        {
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                return Result.Success(entity);
            }
            return Result.Failure<RoleAccess>(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено."));
        }
        catch(Exception ex)
        {
            return Result.Failure<RoleAccess>(new Error(Errors.InternalServerError, "Ошибка при получении предоставления роли из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, int? userId = null, int? roleId = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                entity.UserId = userId ?? entity.UserId;
                entity.RoleId = roleId ?? entity.RoleId;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                return Result.Success(entity);
            }
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено."));
        }
        catch( Exception ex )
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при обновлении предоставления роли в базе данных."));
        }
    }
}
