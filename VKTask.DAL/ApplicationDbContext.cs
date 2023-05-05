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

        }
    }
}
