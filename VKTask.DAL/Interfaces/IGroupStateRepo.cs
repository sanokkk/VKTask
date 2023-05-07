using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.Domain.Models;

namespace VKTask.DAL.Interfaces
{
    public interface IGroupStateRepo
    {
        Task<UserGroup> GetGroupAsync(int id);

        Task<UserState> GetStateAsync(int id);
    }
}
