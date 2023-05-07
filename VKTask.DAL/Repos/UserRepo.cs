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
        public async Task<User> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(User user)
        {
            user.UserState = await _db.UserStates.FirstOrDefaultAsync(s => s.Description == "Blocked");
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByIdAsync(Guid id) => await _db.Users.FirstOrDefaultAsync(f => f.Id == id);

        public async Task<User[]> GetUsers() => await _db.Users.ToArrayAsync();
    }
}
