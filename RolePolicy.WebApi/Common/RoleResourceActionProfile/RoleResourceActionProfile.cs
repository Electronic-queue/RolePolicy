using AutoMapper;
using RolePolicy.Application.RoleResourceActions.Commands.CreateRoleResourceAction;
using RolePolicy.Application.RoleResourceActions.Commands.UpdateRoleResourceAction;
using RolePolicy.WebApi.Contracts.RoleResourceActionsContracts;

namespace RolePolicy.WebApi.Common.RoleResourceActionProfile;

public class RoleResourceActionProfile : Profile
{
    public RoleResourceActionProfile()
    {
        CreateMap<CreateRoleResourceActionDto, CreateRoleResourceActionCommand>()
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
            .ForMember(roleResourceActionVm => roleResourceActionVm.CreatedBy,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.CreatedBy));

        CreateMap<UpdateRoleResourceActionDto, UpdateRoleResourceActionCommand>()
            .ForMember(roleResourceActionVm => roleResourceActionVm.Id,
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.Id))
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
            opt => opt.MapFrom(roleResourceAction => roleResourceAction.DescriptionEn));
    }
}
