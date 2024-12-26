using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class RoleAccessRepository(RolePolicyDbContext dbContext) : IRoleAccessRepository
{
    public async Task<Result> AddAsync(RoleAccess roleAccess)
    {
        try
        {
            await dbContext.AddAsync(roleAccess);
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entity = await dbContext.RoleAccesses.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление роли с id {id} не найдено."));
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
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
            return Result.Failure<List<RoleAccess>>(new Error(Errors.InternalServerError, ex.Message));
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
            return Result.Failure<RoleAccess>(new Error(Errors.InternalServerError, ex.Message));
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
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
