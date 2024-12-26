using AutoMapper;
using RolePolicy.Application.Common.Mappings;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Application.Actions.Queries.GetActionList;

public class ActionLookupDto : IMapWith<Action>
{
    public int ActionId { get; set; }

    public string Name { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Action, ActionLookupDto>()
            .ForMember(actionVm => actionVm.ActionId,
            opt => opt.MapFrom(action => action.ActionId))
            .ForMember(actionVm => actionVm.Name,
            opt => opt.MapFrom(action => action.Name))
            .ForMember(actionVm => actionVm.DescriptionRu,
            opt => opt.MapFrom(action => action.DescriptionRu))
            .ForMember(actionVm => actionVm.DescriptionKk,
            opt => opt.MapFrom(action => action.DescriptionKk))
            .ForMember(actionVm => actionVm.DescriptionEn,
            opt => opt.MapFrom(action => action.DescriptionEn))
            .ForMember(actionVm => actionVm.CreatedOn,
            opt => opt.MapFrom(action => action.CreatedOn))
            .ForMember(actionVm => actionVm.CreatedBy,
            opt => opt.MapFrom(action => action.CreatedBy));
    }
}
