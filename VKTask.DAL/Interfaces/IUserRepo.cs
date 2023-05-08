using VKTask.Domain.Models;

namespace VKTask.DAL.Interfaces;

public interface IUserRepo
{
    Task<User[]> GetUsers();

    Task<User> GetByIdAsync(Guid id);

    Task AddAsync(User user);

    Task DeleteAsync(User user);
}
