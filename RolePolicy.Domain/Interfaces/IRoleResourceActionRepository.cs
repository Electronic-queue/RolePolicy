using KDS.Primitives.FluentResult;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Domain.Interfaces;

public interface IRoleResourceActionRepository
{
    Task<Result<List<RoleResourceAction>>> GetAllAsync();
    Task<Result> AddAsync(RoleResourceAction roleResourceAction);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetById(int id);
    Task<Result> UpdateAsync(int id, int? roleId = null, int? resourceId = null, int? actionId = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null);
}