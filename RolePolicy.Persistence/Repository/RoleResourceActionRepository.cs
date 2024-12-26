using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class RoleResourceActionRepository(RolePolicyDbContext dbContext) : IRoleResourceActionRepository
{
    public async Task<Result> AddAsync(RoleResourceAction roleResourceAction)
    {
        try
        {
            await dbContext.AddAsync(roleResourceAction);
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
            var entity = await dbContext.RoleResourceActions.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure(new Error(Errors.NotFound, $"Связь роли и действий над ресурсами с id {id} не найдена."));
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<RoleResourceAction>>> GetAllAsync()
    {
        try
        {
            var entities = await dbContext.RoleResourceActions.ToListAsync();
            return Result.Success(entities);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<RoleResourceAction>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<RoleResourceAction>> GetById(int id)
    {
        try
        {
            var entity = await dbContext.RoleResourceActions.FindAsync(id);
            if (entity != null)
            {
                return Result.Success(entity);
            }
            return Result.Failure<RoleResourceAction>(new Error(Errors.NotFound, $"Связь роли и действий над ресурсами с id {id} не найдена."));
        }
        catch(Exception ex)
        {
            return Result.Failure<RoleResourceAction>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int id, int? roleId = null, int? resourceId = null, int? actionId = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
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
                return Result.Success(entity);
            }
            return Result.Failure(new Error(Errors.NotFound, $"Связь роли и действий над ресурсами с id {id} не найдена."));
        }
        catch( Exception ex )
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
