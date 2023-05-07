using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKTask.DAL.Interfaces;
using VKTask.Domain.Models;

namespace VKTask.DAL.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _db;
        public UserRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            var newState = await _db.UserStates.FirstOrDefaultAsync(s => s.Code == "Blocked");
            user.UserState = newState;
            user.UserStateId = newState.UserStateId;
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id) => await _db.Users
            .Include(i => i.UserGroup)
            .Include(i => i.UserState)
            .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<User[]> GetUsers() => await _db.Users
            .Include(i => i.UserGroup)
            .Include(i => i.UserState)
            .ToArrayAsync();
    }
}
