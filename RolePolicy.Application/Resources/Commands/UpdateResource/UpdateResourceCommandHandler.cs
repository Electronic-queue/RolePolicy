using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Commands.UpdateResource;

public class UpdateResourceCommandHandler(IResourceRepository resourceRepository) : IRequestHandler<UpdateResourceCommand, Result>
{
    public async Task<Result> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var result =  await resourceRepository.UpdateAsync(request.Id, request.Name, request.DescriptionRu, request.DescriptionKk, request.DescriptionEn);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}