using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class RoleRepository(RolePolicyDbContext dbContext) : IRoleRepository
{
    public async Task<Result> AddAsync(Role role)
    {
        try
        {
            await dbContext.AddAsync(role);
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при добавлении роли в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure(new Error(Errors.NotFound, $"Роль с id {id} не найдена."));
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при удалении роли из базы данных."));
        }
    }

    public async Task<Result<List<Role>>> GetAllAsync()
    {
        try
        {
            var entities = await dbContext.Roles.ToListAsync();
            return Result.Success(entities);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<Role>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка ролей из базы данных."));
        }
    }

    public async Task<Result<Role>> GetById(int id)
    {
        try
        {
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity != null)
            {
                return Result.Success(entity);
            }
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Роль с id {id} не найдена."));
        }
        catch(Exception ex)
        {
            return Result.Failure<Role>(new Error(Errors.InternalServerError, "Ошибка при получении роли из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? nameRu = null, string? nameKk = null, string? nameEn = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
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
                return Result.Success(entity);
            }
            return Result.Failure(new Error(Errors.NotFound, $"Роль с id {id} не найдена."));
        }
        catch( Exception ex )
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при обновлении роли в базе данных."));
        }
    }
}
