using AuthenticationData.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationData
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        //Do not add custom entities
        public DbSet<EntityRole> Roles { get; set; }
        public DbSet<EntityUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityRole>()
                .HasIndex(a => a.RoleName)
                .IsUnique();

            modelBuilder.Entity<EntityUser>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<EntityUser>()
                .HasOne(a => a.Role);
        }
    }
}
