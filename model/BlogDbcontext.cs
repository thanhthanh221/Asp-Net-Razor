using System;
using Microsoft.EntityFrameworkCore;
namespace Razor.model{
    public class Context :DbContext{
        public DbSet<Blog> blogs{set;get;}
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}