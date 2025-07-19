using Microsoft.EntityFrameworkCore;
using OMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Infrastructure.Database.AppDbContexts
{
    public class OMSReadOnlyDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public OMSReadOnlyDbContext(DbContextOptions<OMSReadOnlyDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OMS;Integrated Security=True;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
                entity.HasMany(o => o.OrderDetails)
                      .WithOne()
                      .HasForeignKey(od => od.OrderId)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa cascade cho OrderDetails
            });

            // Cấu hình OrderDetail
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => od.OrderDetailId);
                entity.Property(od => od.ProductCode).IsRequired().HasMaxLength(50);
                entity.Property(od => od.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(od => od.Price).HasPrecision(18, 2);
                entity.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey(od => od.ProductId)
                      .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Product nếu được tham chiếu
            });

            // Cấu hình Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductCode).IsRequired().HasMaxLength(50);
                entity.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasPrecision(18, 2);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OMSReadOnlyDbContext).Assembly);
        }
    }
}
