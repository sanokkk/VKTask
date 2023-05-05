using Microsoft.EntityFrameworkCore;
using VKTask.Domain.Models;

namespace VKTask.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<UserState> UserStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne<UserGroup>(o => o.UserGroup)
                .WithMany()
                .HasForeignKey(f => f.UserGroupId);
            modelBuilder.Entity<User>().HasOne<UserState>(o => o.UserState)
                .WithMany()
                .HasForeignKey(f => f.UserStateId);

            modelBuilder.Entity<User>().HasKey(k => k.Id);
            modelBuilder.Entity<UserGroup>().HasKey(k => k.Id);
            modelBuilder.Entity<UserState>().HasKey(k => k.Id);

            var userStates = new UserState[]
            {
                new UserState()
                {
                    Id = 1,
                    Code = 1,
                    Description = "Active"
                },
                new UserState()
                {
                    Id = 2,
                    Code = 2,
                    Description = "Blocked"
                }
            };

            var userGroups = new UserGroup[] {
                new UserGroup()
                {
                    Id = 1,
                    Code = 1,
                    Description = "Admin"
                },
                new UserGroup()
                {
                    Id = 2,
                    Code = 2,
                    Description = "User"
                }
            };

            modelBuilder.Entity<UserGroup>().HasData(userGroups);
            modelBuilder.Entity<UserState>().HasData(userStates);

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = Guid.NewGuid() ,
                Login = "sanokkk" ,
                Password = "20020918", 
                CreatedDate = DateTime.UtcNow,
                UserGroupId = userGroups[0].Id,
                UserStateId = userStates[0].Id
            });

        }
    }
}
