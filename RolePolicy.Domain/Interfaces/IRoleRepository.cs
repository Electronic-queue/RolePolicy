using KDS.Primitives.FluentResult;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Result<List<Role>>> GetAllAsync();
    Task<Result> AddAsync(Role role);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetById(int id);
    Task<Result> UpdateAsync(int id, string? nameRu = null, string? nameKk = null, string? nameEn = null, 
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null);
}