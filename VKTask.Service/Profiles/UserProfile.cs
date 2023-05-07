using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.DAL;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;

namespace VKTask.Service.Profiles
{
    public class UserProfile: Profile
    {
        private readonly ApplicationDbContext _context;
        public UserProfile()
        {
            
        }
        public UserProfile(ApplicationDbContext context)
        {
            _context = context;
            CreateMap<CreateUserDto, User>()
                .ForMember(dst => dst.UserGroup,
                opt =>
                {
                    opt.MapFrom(src => _context.UserGroups.First(f => f.UserGroupId == src.UserGroupId));
                })
                .ForMember(dst => dst.CreatedDate, opt =>
                {
                    opt.MapFrom(time => DateTime.UtcNow);
                })
                .ForMember(dst => dst.Id,
                opt =>
                {
                    opt.MapFrom(src => Guid.NewGuid());
                })
                .ForMember(dst => dst.UserGroupId,
                opt =>
                {
                    opt.MapFrom(src => src.UserGroupId);
                })
                .ForMember(dst => dst.Login,
                opt =>
                {
                    opt.MapFrom(src => src.Login);
                })
                .ForMember(dst => dst.Password,
                opt =>
                {
                    opt.MapFrom(src => src.Password);
                });
        }
    }
}
