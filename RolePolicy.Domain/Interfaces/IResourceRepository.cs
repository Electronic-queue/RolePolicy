using KDS.Primitives.FluentResult;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Domain.Interfaces;

public interface IResourceRepository
{
    Task<Result<List<Resource>>> GetAllAsync();
    Task<Result> AddAsync(Resource resource);
    Task<Result> DeleteAsync(int id);
    Task<Result<Resource>> GetById(int id);
    Task<Result> UpdateAsync(int id, string? name = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null);
}
