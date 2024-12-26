using AutoMapper;
using RolePolicy.Application.Roles.Commands.CreateRole;
using RolePolicy.Application.Roles.Commands.UpdateRole;
using RolePolicy.Application.Users.Commands.CreateUser;
using RolePolicy.Application.Users.Commands.UpdateUser;
using RolePolicy.WebApi.Contracts.RolesContracts;
using RolePolicy.WebApi.Contracts.UsersContracts;

namespace RolePolicy.WebApi.Common.RoleProfile;

public class RoleProfile : Profile
{
    public RoleProfile()
    {

        CreateMap<CreateRoleDto, CreateRoleCommand>()
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
            .ForMember(RoleVm => RoleVm.CreatedBy,
            opt => opt.MapFrom(Role => Role.CreatedBy));

        CreateMap<UpdateRoleDto, UpdateRoleCommand>()
            .ForMember(RoleVm => RoleVm.Id,
            opt => opt.MapFrom(Role => Role.Id))
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
            opt => opt.MapFrom(Role => Role.DescriptionEn));
    }
}
