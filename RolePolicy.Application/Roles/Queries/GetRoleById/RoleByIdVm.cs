using AutoMapper;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Roles.Queries.GetRoleById;

public class RoleByIdVm : IMapWith<Role>
{
    public int RoleId { get; set; }

    public string NameRu { get; set; } = null!;

    public string NameKk { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleByIdVm>()
            .ForMember(RoleVm => RoleVm.RoleId,
            opt => opt.MapFrom(Role => Role.RoleId))
            .ForMember(RoleVm => RoleVm.NameRu,
            opt => opt.MapFrom(Role => Role.NameRu))
            .ForMember(RoleVm => RoleVm.NameKk,
            opt => opt.MapFrom(Role => Role.NameKk))
            .ForMember(RoleVm => RoleVm.NameEn,
            opt => opt.MapFrom(Role => Role.NameEn))
            .ForMember(RoleVm => RoleVm.DescriptionRu,
            opt => opt.MapFrom(Role => Role.DescriptionRu))
            .ForMember(RoleVm => RoleVm.DescriptionKk,
            opt => opt.MapFrom(Role => Role.DescriptionKk))
            .ForMember(RoleVm => RoleVm.DescriptionEn,
            opt => opt.MapFrom(Role => Role.DescriptionEn))
            .ForMember(RoleVm => RoleVm.CreatedOn,
            opt => opt.MapFrom(Role => Role.CreatedOn))
            .ForMember(RoleVm => RoleVm.CreatedBy,
            opt => opt.MapFrom(Role => Role.CreatedBy));
    }
}
