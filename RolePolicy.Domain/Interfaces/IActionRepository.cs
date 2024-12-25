using KDS.Primitives.FluentResult;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Domain.Interfaces;

public interface IActionRepository
{
    Task<Result<List<Action>>> GetAllAsync();
    Task<Result> AddAsync(Action action);
    Task<Result> DeleteAsync(int id);
    Task<Result<Action>> GetById(int id);
    Task<Result> UpdateAsync(int id, string? name = null,
        string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null);
}
