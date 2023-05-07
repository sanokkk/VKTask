﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.DAL.Interfaces;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;
using VKTask.Service.Interfaces;

namespace VKTask.Service.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _users;
        private readonly IMapper _mapper;
        private readonly IGroupStateRepo _groupStateRepo;
        public UserService(IUserRepo users, IMapper mapper, IGroupStateRepo groupStateRepo)
        {
            _users = users;
            _mapper = mapper;
            _groupStateRepo = groupStateRepo;
        }

        public async Task<User> CreateUserAsync(CreateUserDto userModel)
        {
            //var user = _mapper.Map<User>(userModel);
            var user = await MapToUser(userModel);
            if (await IsSameLoginInDbAsync(user.Login))
                throw new ArgumentException("Login already exists");
            if (user.UserGroup.Code == "Admin" && !await CanBeAdminAsync())
                throw new ArgumentException("System can't contains 2 or more admins");
            await _users.AddAsync(user);
            return user;

        }

        private async Task<bool> IsSameLoginInDbAsync(string login)
        {
            var users = await _users.GetUsers();
            if (users.Any(u => u.Login == login))
                return true;
            return false;
        }

        private async Task<bool> CanBeAdminAsync()
        {
            var users = await _users.GetUsers();
            if (users.Any(u => u.UserGroup.Code == "Admin"))
                return false;
            return true;
        }

        private async Task<User> MapToUser(CreateUserDto userModel)
        {
            var group = await _groupStateRepo.GetGroupAsync(userModel.UserGroupId);
            var state = await _groupStateRepo.GetStateAsync(1);
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Login = userModel.Login,
                Password = userModel.Password,
                UserGroupId = group.UserGroupId,
                UserGroup = group,
                UserStateId = state.UserStateId,
                UserState = state,
                CreatedDate = DateTime.UtcNow,
            };
            return user;
        }
    }
}
