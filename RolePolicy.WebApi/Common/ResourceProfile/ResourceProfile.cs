using AutoMapper;
using RolePolicy.Application.Resources.Commands.CreateResource;
using RolePolicy.Application.Resources.Commands.UpdateResource;
using RolePolicy.WebApi.Contracts.ResourcesContracts;

namespace RolePolicy.WebApi.Common.ResourceProfile;

public class ResourceProfile : Profile
{
    public ResourceProfile()
    {
        CreateMap<CreateResourceDto, CreateResourceCommand>()
            .ForMember(resourceVm => resourceVm.Name,
            opt => opt.MapFrom(resource => resource.Name))
            .ForMember(resourceVm => resourceVm.DescriptionRu,
            opt => opt.MapFrom(resource => resource.DescriptionRu))
            .ForMember(resourceVm => resourceVm.DescriptionKk,
            opt => opt.MapFrom(resource => resource.DescriptionKk))
            .ForMember(resourceVm => resourceVm.DescriptionEn,
            opt => opt.MapFrom(resource => resource.DescriptionEn))
            .ForMember(resourceVm => resourceVm.CreatedBy,
            opt => opt.MapFrom(resource => resource.CreatedBy));

        CreateMap<UpdateResourceDto, UpdateResourceCommand>()
            .ForMember(resourceVm => resourceVm.Id,
            opt => opt.MapFrom(resource => resource.Id))
            .ForMember(resourceVm => resourceVm.Name,
            opt => opt.MapFrom(resource => resource.Name))
            .ForMember(resourceVm => resourceVm.DescriptionRu,
            opt => opt.MapFrom(resource => resource.DescriptionRu))
            .ForMember(resourceVm => resourceVm.DescriptionKk,
            opt => opt.MapFrom(resource => resource.DescriptionKk))
            .ForMember(resourceVm => resourceVm.DescriptionEn,
            opt => opt.MapFrom(resource => resource.DescriptionEn));
    }
}
