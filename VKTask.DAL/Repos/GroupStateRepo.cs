using Microsoft.EntityFrameworkCore;
using VKTask.DAL.Interfaces;
using VKTask.Domain.Models;

namespace VKTask.DAL.Repos;

public class GroupStateRepo : IGroupStateRepo
{
    private readonly ApplicationDbContext _context;

    public GroupStateRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserGroup> GetGroupAsync(int id) => await _context.UserGroups.FirstOrDefaultAsync(g => g.UserGroupId == id);

    public async Task<UserState> GetStateAsync(int id) => await _context.UserStates.FirstOrDefaultAsync(s => s.UserStateId == id);
}
