using KDS.Primitives.FluentResult;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Domain.Interfaces;

public interface IUserRepository
{
    Task<Result<List<User>>> GetAllAsync();
    Task<Result> AddAsync(User user);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetById(int id);
    Task<Result> UpdateAsync(int id, string? firstName = null, string? lastName = null, string? surname = null, 
        string? login = null, string? passwordHash = null, bool? isDeleted = null);
}