using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Entities;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Queries.GetResourceById;

public class GetResourceByIdQueryHandler(IResourceRepository ResourceRepository) : IRequestHandler<GetResourceByIdQuery, Result<Resource>>
{
    public async Task<Result<Resource>> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await ResourceRepository.GetById(request.Id);
        if (record.IsFailed)
        {
            return Result.Failure<Resource>(record.Error);
        }
        return record;
    }
}
