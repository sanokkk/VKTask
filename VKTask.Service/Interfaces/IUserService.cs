using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;

namespace VKTask.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDto userModel, CancellationTokenSource source);

        Task<User> DeleteAsync(string requestedUserId, Guid id);

        Task<User[]> GetAsync(int page = 0);

        Task<User> GetByIdAsync(Guid id);
    }
}
