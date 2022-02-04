using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Razor.model{
    public class Context :IdentityDbContext<AppUser> {
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
            modelBuilder.Entity<HoaDon>(entity=>{
                entity.HasOne(p=> p.shipper).WithMany(c=> c.hoaDon).HasForeignKey("ID_Shiper").OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Product>(entity =>{
                entity.HasOne(p=>p.kho).WithMany(c =>c.Products).HasForeignKey("MaKho").OnDelete(DeleteBehavior.Cascade);

            });
        }
        public DbSet<Shipper> shippers{set;get;}
        public DbSet<Product> products{set;get;}
        public DbSet<HoaDon> hoaDons{set;get;}
        public DbSet<Kho> khos{set;get;}


    }


}