using AutoMapper;
using VKTask.DAL;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;

namespace VKTask.Service.Profiles;

public class UserProfile: Profile
{
    private readonly ApplicationDbContext _context;
    public UserProfile()
    {
        
    }
    public UserProfile(ApplicationDbContext context)
    {
        _context = context;
        CreateMap<User, CreateUserDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(dst => dst.Id,
            opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dst => dst.UserState,
            opt =>
            {
                opt.MapFrom(src => _context.UserStates.FirstOrDefault(f => f.Code == "Active"));
            })
            .ForMember(dst => dst.UserGroup,
            opt =>
            {
                opt.MapFrom(src => _context.UserGroups.FirstOrDefault(f => f.UserGroupId == src.UserGroupId));
            })
            .ForMember(dst => dst.CreatedDate,
            opt => opt.MapFrom(src => DateTime.UtcNow));

    }
}
