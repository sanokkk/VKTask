using Microsoft.EntityFrameworkCore;
using VKTask.Domain.Models;

namespace VKTask.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options)
        {
            //Database.EnsureDeleted();
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
            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder.Entity<UserGroup>().HasKey(k => k.UserGroupId);
            modelBuilder.Entity<UserGroup>().ToTable("user_groups");

            modelBuilder.Entity<UserState>().HasKey(k => k.UserStateId);
            modelBuilder.Entity<UserState>().ToTable("user_states");

            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup()
            {
                UserGroupId = 1,
                Code = "Admin",
                Description = "User which has all rights for actions"
            }, new UserGroup()
            {
                UserGroupId = 2,
                Code = "User",
                Description = "Common user"
            });
            modelBuilder.Entity<UserState>().HasData(
                new UserState()
                {
                    UserStateId = 1,
                    Code = "Active",
                    Description = "Active user"
                },
                new UserState()
                {
                    UserStateId = 2,
                    Code = "Blocked",
                    Description = "Blocked user (deleted)"
                });

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = Guid.NewGuid() ,
                Login = "sanokkk" ,
                Password = "20020918", 
                CreatedDate = DateTime.UtcNow,
                UserGroupId = 1,
                UserStateId = 1            
            });

        }
    }
}
