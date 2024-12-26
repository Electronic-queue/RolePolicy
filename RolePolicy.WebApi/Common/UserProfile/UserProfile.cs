using AutoMapper;
using RolePolicy.Application.Users.Commands.CreateUser;
using RolePolicy.Application.Users.Commands.UpdateUser;
using RolePolicy.WebApi.Contracts.UsersContracts;

namespace RolePolicy.WebApi.Common.UserProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, CreateUserCommand>()
            .ForMember(UserVm => UserVm.FirstName,
            opt => opt.MapFrom(User => User.FirstName))
            .ForMember(UserVm => UserVm.LastName,
            opt => opt.MapFrom(User => User.LastName))
            .ForMember(UserVm => UserVm.Surname,
            opt => opt.MapFrom(User => User.Surname))
            .ForMember(UserVm => UserVm.Login,
            opt => opt.MapFrom(User => User.Login))
            .ForMember(UserVm => UserVm.PasswordHash,
            opt => opt.MapFrom(User => User.PasswordHash))
            .ForMember(UserVm => UserVm.CreatedBy,
            opt => opt.MapFrom(User => User.CreatedBy));

        CreateMap<UpdateUserDto, UpdateUserCommand>()
            .ForMember(UserVm => UserVm.Id,
            opt => opt.MapFrom(User => User.Id))
            .ForMember(UserVm => UserVm.FirstName,
            opt => opt.MapFrom(User => User.FirstName))
            .ForMember(UserVm => UserVm.LastName,
            opt => opt.MapFrom(User => User.LastName))
            .ForMember(UserVm => UserVm.Surname,
            opt => opt.MapFrom(User => User.Surname))
            .ForMember(UserVm => UserVm.Login,
            opt => opt.MapFrom(User => User.Login))
            .ForMember(UserVm => UserVm.PasswordHash,
            opt => opt.MapFrom(User => User.PasswordHash))
            .ForMember(UserVm => UserVm.IsDeleted,
            opt => opt.MapFrom(User => User.IsDeleted));
    }
}

