using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;


namespace RolePolicy.Persistence.Repository;

public class UserRepository(RolePolicyDbContext dbContext) : IUserRepository
{
    public async Task<Result> AddAsync(User user)
    {
        try
        {
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при добавлении пользователя в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entity = await dbContext.Users.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure(new Error(Errors.NotFound, $"Пользователь с id {id} не найден."));
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при удалении пользователя из базы данных."));
        }
    }

    public async Task<Result<List<User>>> GetAllAsync()
    {
        try
        {
            var entities = await dbContext.Users.ToListAsync();
            return Result.Success(entities);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<User>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка пользователей из базы данных."));
        }
    }

    public async Task<Result<User>> GetById(int id)
    {
        try
        {
            var entity = await dbContext.Users.FindAsync(id);
            if (entity != null)
            {
                return Result.Success(entity);
            }
            return Result.Failure<User>(new Error(Errors.NotFound, $"Пользователь с id {id} не найден."));
        }
        catch(Exception ex)
        {
            return Result.Failure<User>(new Error(Errors.InternalServerError, "Ошибка при получении пользователя из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? firstName = null, string? lastName = null, string? surname = null,
        string? login = null, string? passwordHash = null, bool? isDeleted = null)
    {
        try
        {
            var entity = await dbContext.Users.FindAsync(id);
            if (entity != null)
            {
                entity.FirstName = firstName ?? entity.FirstName;
                entity.LastName = lastName ?? entity.LastName;
                entity.Surname = surname ?? entity.Surname;
                entity.Login = login ?? entity.Login;
                entity.PasswordHash = passwordHash ?? entity.PasswordHash;
                entity.IsDeleted = isDeleted ?? entity.IsDeleted;
                await dbContext.SaveChangesAsync();
                return Result.Success(entity);
            }
            return Result.Failure(new Error(Errors.NotFound, $"Пользователь с id {id} не найден."));
        }
        catch( Exception ex )
        {
            return Result.Failure(new Error(Errors.InternalServerError, "Ошибка при обновлении пользователя в базе данных."));
        }
    }
}
