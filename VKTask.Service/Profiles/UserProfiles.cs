using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using VKTask.DAL;
using VKTask.DAL.Interfaces;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;

namespace VKTask.Service.Profiles
{
    public class UserProfiles : Profile
    {
        private readonly IGroupStateRepo _context;
        public UserProfiles(IGroupStateRepo context)
        {
            _context = context;
        }
        public UserProfiles()
        {
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>()
            .ForMember(dst => dst.Id,
            opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dst => dst.UserStateId,
            opt =>
                opt.MapFrom(src => 2))
            .ForMember(dst => dst.UserGroupId,
            opt =>
            {
                opt.MapFrom(src => src.UserGroupId);
            })
            .ForMember(dst => dst.CreatedDate,
            opt => opt.MapFrom(src => DateTime.UtcNow))
            ;
        }
    }
}
