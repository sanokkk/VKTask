using AutoMapper;
using VKTask.DAL.Interfaces;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;

namespace VKTask.Service.Profiles;

public class UserProfiles : Profile
{
    private readonly IGroupStateRepo _db;
    public UserProfiles()
    {
        
    }
    public UserProfiles(IGroupStateRepo db)
    {
        CreateMap<User, CreateUserDto>();
        _db = db;
        CreateMap<CreateUserDto, User>()
        .ForMember(dst => dst.Id,
        opt => opt.MapFrom(src => Guid.NewGuid()))
        .ForMember(dst => dst.UserStateId,
        opt =>
            opt.MapFrom(src => 21))
        .ForMember(dst => dst.UserState,
        opt =>
        {
            opt.MapFrom(src => GetState());
        })
        .ForMember(dst => dst.UserGroupId,
        opt =>
        {
            opt.MapFrom(src => src.UserGroupId);
        })
        .ForMember(dst => dst.UserGroup,
        opt =>
        {
            opt.MapFrom(src => GetGroup(src.UserGroupId));
        })
        .ForMember(dst => dst.CreatedDate,
        opt => opt.MapFrom(src => DateTime.UtcNow))
        ;
    }
    private UserGroup GetGroup(int id)
    {
        var group = _db.GetGroupAsync(id).Result;
        return group;
    }

    private UserState GetState()
    {
        var state = _db.GetStateAsync(1).Result;
        return state;
    }
}
