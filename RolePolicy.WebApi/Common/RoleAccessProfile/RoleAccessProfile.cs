using AutoMapper;
using RolePolicy.Application.RoleAccesses.Commands.CreateRoleAccess;
using RolePolicy.Application.RoleAccesses.Commands.UpdateRoleAccess;
using RolePolicy.WebApi.Contracts.RoleAccessesContracts;

namespace RolePolicy.WebApi.Common.RoleAccessProfile;

public class RoleAccessProfile : Profile
{
    public RoleAccessProfile()
    {
        CreateMap<CreateRoleAccessDto, CreateRoleAccessCommand>()
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
            .ForMember(roleAccessVm => roleAccessVm.GivenBy,
            opt => opt.MapFrom(roleAccess => roleAccess.GivenBy));

        CreateMap<UpdateRoleAccessDto, UpdateRoleAccessCommand>()
            .ForMember(roleAccessVm => roleAccessVm.Id,
            opt => opt.MapFrom(roleAccess => roleAccess.Id))
            .ForMember(roleAccessVm => roleAccessVm.UserId,
            opt => opt.MapFrom(roleAccess => roleAccess.UserId))
            .ForMember(roleAccessVm => roleAccessVm.RoleId,
            opt => opt.MapFrom(roleAccess => roleAccess.RoleId))
            .ForMember(roleAccessVm => roleAccessVm.DescriptionRu,
            opt => opt.MapFrom(roleAccess => roleAccess.DescriptionRu))
            .ForMember(roleAccessVm => roleAccessVm.DescriptionKk,
            opt => opt.MapFrom(roleAccess => roleAccess.DescriptionKk))
            .ForMember(roleAccessVm => roleAccessVm.DescriptionEn,
            opt => opt.MapFrom(roleAccess => roleAccess.DescriptionEn));
    }
}
