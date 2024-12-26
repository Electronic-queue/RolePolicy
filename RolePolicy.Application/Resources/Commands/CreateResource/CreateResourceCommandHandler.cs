using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Commands.CreateResource;

public class CreateResourceCommandHandler(IResourceRepository resourceRepository) : IRequestHandler<CreateResourceCommand, Result>
{
    public async Task<Result> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = new Domain.Entities.Resource {
            Name = request.Name,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result =  await resourceRepository.AddAsync(resource);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}