using AutoMapper;
using RolePolicy.Application.Actions.Commands.CreateAction;
using RolePolicy.Application.Actions.Commands.UpdateAction;
using RolePolicy.WebApi.Contracts.ActionsContracts;

namespace RolePolicy.WebApi.Common.ActionProfile;

public class ActionProfile : Profile
{
    public ActionProfile()
    {
        CreateMap<CreateActionDto, CreateActionCommand>()
            .ForMember(actionVm => actionVm.Name,
            opt => opt.MapFrom(action => action.Name))
            .ForMember(actionVm => actionVm.DescriptionRu,
            opt => opt.MapFrom(action => action.DescriptionRu))
            .ForMember(actionVm => actionVm.DescriptionKk,
            opt => opt.MapFrom(action => action.DescriptionKk))
            .ForMember(actionVm => actionVm.DescriptionEn,
            opt => opt.MapFrom(action => action.DescriptionEn))
            .ForMember(actionVm => actionVm.CreatedBy,
            opt => opt.MapFrom(action => action.CreatedBy));

        CreateMap<UpdateActionDto, UpdateActionCommand>()
            .ForMember(actionVm => actionVm.Id,
            opt => opt.MapFrom(action => action.Id))
            .ForMember(actionVm => actionVm.Name,
            opt => opt.MapFrom(action => action.Name))
            .ForMember(actionVm => actionVm.DescriptionRu,
            opt => opt.MapFrom(action => action.DescriptionRu))
            .ForMember(actionVm => actionVm.DescriptionKk,
            opt => opt.MapFrom(action => action.DescriptionKk))
            .ForMember(actionVm => actionVm.DescriptionEn,
            opt => opt.MapFrom(action => action.DescriptionEn));
    }
}
