using VKTask.Domain.Models;

namespace VKTask.DAL.Interfaces;

public interface IGroupStateRepo
{
    Task<UserGroup> GetGroupAsync(int id);

    Task<UserState> GetStateAsync(int id);
}
