using KDS.Primitives.FluentResult;
using MediatR;
using RolePolicy.Domain.Commom.Exceptions;
using RolePolicy.Domain.Interfaces;

namespace RolePolicy.Application.Resources.Commands.DeleteResource;

public class DeleteResourceCommandHandler(IResourceRepository resourceRepository) : IRequestHandler<DeleteResourceCommand, Result>
{
    public async Task<Result> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var result =  await resourceRepository.DeleteAsync(request.Id);
        if (result.IsFailed)
        {
            return Result.Failure(result.Error);
        }
        return Result.Success();
    }
}