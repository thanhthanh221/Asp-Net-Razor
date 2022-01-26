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
                entity.HasOne(p =>p.nhanVien).WithMany(c =>c.HoaDons).HasForeignKey("MaNhanVienBan").OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(p=>p.KhachHang).WithMany(c =>c.HoaDons).HasForeignKey("NguoiMua").OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Product>(entity =>{
                entity.HasOne(p=>p.kho).WithMany(c =>c.Products).HasForeignKey("MaKho").OnDelete(DeleteBehavior.Cascade);

            });
        }
        public DbSet<Blog> blogs{set;get;}
        public DbSet<Product> products{set;get;}
        public DbSet<NhanVien> NhanViens{set;get;}
        public DbSet<HoaDon> hoaDons{set;get;}
        public DbSet<KhachHang> khachHangs{set;get;}
        public DbSet<Kho> khos{set;get;}


    }


}