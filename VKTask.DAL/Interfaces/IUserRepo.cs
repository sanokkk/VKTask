﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.Domain.Models;

namespace VKTask.DAL.Interfaces
{
    public interface IUserRepo
    {
        Task<User[]> GetUsers();

        Task<User> GetByIdAsync(Guid id);

        Task<User> AddAsync(User user);

        Task<User> DeleteAsync(User user);
    }
}