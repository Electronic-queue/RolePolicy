using KDS.Primitives.FluentResult;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Domain.Interfaces;

public interface IRoleAccessRepository
{
    Task<Result<List<RoleAccess>>> GetAllAsync();
    Task<Result> AddAsync(RoleAccess roleAccess);
    Task<Result> DeleteAsync(int id);
    Task<Result<RoleAccess>> GetById(int id);
    Task<Result> UpdateAsync(int id, int? userId = null, int? roleId = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null);
}