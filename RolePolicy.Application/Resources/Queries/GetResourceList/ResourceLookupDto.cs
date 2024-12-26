using AutoMapper;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Resources.Queries.GetResourceList;

public class ResourceLookupDto : IMapWith<Resource>
{
    public int ResourceId { get; set; }

    public string Name { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Resource, ResourceLookupDto>()
            .ForMember(resourceVm => resourceVm.ResourceId,
            opt => opt.MapFrom(resource => resource.ResourceId))
            .ForMember(resourceVm => resourceVm.Name,
            opt => opt.MapFrom(resource => resource.Name))
            .ForMember(resourceVm => resourceVm.DescriptionRu,
            opt => opt.MapFrom(resource => resource.DescriptionRu))
            .ForMember(resourceVm => resourceVm.DescriptionKk,
            opt => opt.MapFrom(resource => resource.DescriptionKk))
            .ForMember(resourceVm => resourceVm.DescriptionEn,
            opt => opt.MapFrom(resource => resource.DescriptionEn))
            .ForMember(resourceVm => resourceVm.CreatedOn,
            opt => opt.MapFrom(resource => resource.CreatedOn))
            .ForMember(resourceVm => resourceVm.CreatedBy,
            opt => opt.MapFrom(resource => resource.CreatedBy));
    }
}
