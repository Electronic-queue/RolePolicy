using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;
using Action = RolePolicy.Domain.Entities.Action;


namespace RolePolicy.Persistence.Repository;

public class ActionRepository(RolePolicyDbContext dbContext) : IActionRepository
{
    public async Task<Result> AddAsync(Action action)
    {
        try
        {
            await dbContext.AddAsync(action);
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
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {id} не найдено."));
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<Action>>> GetAllAsync()
    {
        try
        {
            var entities = await dbContext.Actions.ToListAsync();
            return Result.Success(entities);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<Action>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<Action>> GetById(int id)
    {
        try
        {
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                return Result.Success(entity);
            }
            return Result.Failure<Action>(new Error(Errors.NotFound, $"Действие с id {id} не найдено."));
        }
        catch(Exception ex)
        {
            return Result.Failure<Action>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? name = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            var entity = await dbContext.Actions.FindAsync(id);
            if (entity != null)
            {
                entity.Name = name ?? entity.Name;
                entity.DescriptionRu = descriptionRu ?? entity.DescriptionRu;
                entity.DescriptionKk = descriptionKk ?? entity.DescriptionKk;
                entity.DescriptionEn = descriptionEn ?? entity.DescriptionEn;
                await dbContext.SaveChangesAsync();
                return Result.Success(entity);
            }
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {id} не найдено."));
        }
        catch( Exception ex )
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
