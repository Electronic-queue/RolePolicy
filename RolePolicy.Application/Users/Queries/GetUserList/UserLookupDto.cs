using AutoMapper;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.Domain.Entities;

namespace RolePolicy.Application.Users.Queries.GetUserList;

public class UserLookupDto : IMapWith<User>
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Surname { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserLookupDto>()
            .ForMember(userVm => userVm.UserId,
            opt => opt.MapFrom(user => user.UserId))
            .ForMember(userVm => userVm.FirstName,
            opt => opt.MapFrom(user => user.FirstName))
            .ForMember(userVm => userVm.LastName,
            opt => opt.MapFrom(user => user.LastName))
            .ForMember(userVm => userVm.Surname,
            opt => opt.MapFrom(user => user.Surname))
            .ForMember(userVm => userVm.Login,
            opt => opt.MapFrom(user => user.Login))
            .ForMember(userVm => userVm.PasswordHash,
            opt => opt.MapFrom(user => user.PasswordHash))
            .ForMember(userVm => userVm.CreatedOn,
            opt => opt.MapFrom(user => user.CreatedOn))
            .ForMember(userVm => userVm.CreatedBy,
            opt => opt.MapFrom(user => user.CreatedBy))
            .ForMember(userVm => userVm.IsDeleted,
            opt => opt.MapFrom(user => user.IsDeleted));
    }
}
