using AutoMapper;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionById;

public class RoleResourceActionByIdVm : IMapWith<RoleResourceAction>
{
    public int RoleResourceActionId { get; set; }

    public int RoleId { get; set; }

    public int ResourceId { get; set; }

    public int ActionId { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleResourceAction, RoleResourceActionByIdVm>()
            .ForMember(roleResourceActionVm => roleResourceActionVm.RoleResourceActionId,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.RoleResourceActionId))
            .ForMember(roleResourceActionVm => roleResourceActionVm.RoleId,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.RoleId))
            .ForMember(roleResourceActionVm => roleResourceActionVm.ResourceId,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.ResourceId))
            .ForMember(roleResourceActionVm => roleResourceActionVm.ActionId,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.ActionId))
            .ForMember(roleResourceActionVm => roleResourceActionVm.DescriptionRu,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.DescriptionRu))
            .ForMember(roleResourceActionVm => roleResourceActionVm.DescriptionKk,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.DescriptionKk))
            .ForMember(roleResourceActionVm => roleResourceActionVm.DescriptionEn,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.DescriptionEn))
            .ForMember(roleResourceActionVm => roleResourceActionVm.CreatedOn,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.CreatedOn))
            .ForMember(roleResourceActionVm => roleResourceActionVm.CreatedBy,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.CreatedBy));
    }
}
