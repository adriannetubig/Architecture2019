using ErrorLoggerData.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ErrorLoggerData
{
    public class ErrorLoggerContext : DbContext
    {
        public ErrorLoggerContext(DbContextOptions<ErrorLoggerContext> options)
            : base(options)
        {
        }
        //Do not add custom entities
        public DbSet<EntityExceptionLog> ExceptionLogs { get; set; }
        public DbSet<EntityInnerExceptionLog> InnerExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityExceptionLog>()
                .HasOne(a => a.InnerExceptionLog);

            modelBuilder.Entity<EntityInnerExceptionLog>()
                .Ignore(a => a.ApplicationName);
        }
    }
}
