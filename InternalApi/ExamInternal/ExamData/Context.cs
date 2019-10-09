using ExamData.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExamData
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        //Do not add custom entities
        public DbSet<EntityFibonacci> Fibonaccis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
