using AutoMapper;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessById;

public class RoleAccessByIdVm : IMapWith<RoleAccess>
{
    public int RoleAccessId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? GivenBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleAccess, RoleAccessByIdVm>()
            .ForMember(roleAccessVm => roleAccessVm.RoleAccessId,
            opt => opt.MapFrom(roleAccess => roleAccess.RoleAccessId))
            .ForMember(roleAccessVm => roleAccessVm.UserId,
            opt => opt.MapFrom(roleAccess => roleAccess.UserId))
            .ForMember(roleAccessVm => roleAccessVm.RoleId,
            opt => opt.MapFrom(roleAccess => roleAccess.RoleId))
            .ForMember(roleAccessVm => roleAccessVm.DescriptionRu,
            opt => opt.MapFrom(roleAccess => roleAccess.DescriptionRu))
            .ForMember(roleAccessVm => roleAccessVm.DescriptionKk,
            opt => opt.MapFrom(roleAccess => roleAccess.DescriptionKk))
            .ForMember(roleAccessVm => roleAccessVm.DescriptionEn,
            opt => opt.MapFrom(roleAccess => roleAccess.DescriptionEn))
            .ForMember(roleAccessVm => roleAccessVm.CreatedOn,
            opt => opt.MapFrom(roleAccess => roleAccess.CreatedOn))
            .ForMember(roleAccessVm => roleAccessVm.GivenBy,
            opt => opt.MapFrom(roleAccess => roleAccess.GivenBy));
    }
}
