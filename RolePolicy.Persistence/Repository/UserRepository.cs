using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;
using System.Data;


namespace RolePolicy.Persistence.Repository;

public class UserRepository(RolePolicyDbContext dbContext, ILogger<UserRepository> logger) : IUserRepository
{
    public async Task<Result> AddAsync(User user)
    {
        try
        {
            logger.LogInformation("Добавление нового пользователя в базу данных.");
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Пользователь {TargetFirstName} {TargetLastName} добавлен в базу данных.", user.FirstName, user.LastName);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении пользователя {TargetFirstName} {TargetLastName} в базу данных.", user.FirstName, user.LastName);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении пользователя {user.FirstName} {user.LastName} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление пользователя из базы данных.");
            var entity = await dbContext.Users.FindAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Пользователь с id {TargetUserId} удален из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Пользователь с id {TargetUserId} не найден в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Пользователь с id {id} не найден в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении пользователя с id {TargetUserId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении пользователя с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<User>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка пользователей из базы данных.");
            var entities = await dbContext.Users.ToListAsync();
            logger.LogInformation("Полный список пользователей получен из базы данных.");
            return Result.Success(entities);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка пользователей из базы данных.");
            return Result.Failure<List<User>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка пользователей из базы данных."));
        }
    }

    public async Task<Result<User>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Получение пользователя из базы данных.");
            var entity = await dbContext.Users.FindAsync(id);
            if (entity != null)
            {
                logger.LogInformation("Пользователь с id {TargetUserId} получен из базы данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Пользователь с id {TargetUserId} не найден в базе данных.", id);
            return Result.Failure<User>(new Error(Errors.NotFound, $"Пользователь с id {id} не найден в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении пользователя с id {TargetUserId} из базы данных.", id);
            return Result.Failure<User>(new Error(Errors.InternalServerError, $"Ошибка при получении пользователя с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int id, string? firstName = null, string? lastName = null, string? surname = null,
        string? login = null, string? passwordHash = null, bool? isDeleted = null)
    {
        try
        {
            logger.LogInformation("Обновление пользователя в базе данных.");
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
                logger.LogInformation("Пользователь с id {TargetUserId} обновлен в базе данных.", id);
                return Result.Success(entity);
            }
            logger.LogError("Пользователь с id {TargetUserId} не найден в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Пользователь с id {id} не найден в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении пользователя с id {TargetUserId} в базе данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении пользователя с id {id} в базе данных."));
        }
    }
}
