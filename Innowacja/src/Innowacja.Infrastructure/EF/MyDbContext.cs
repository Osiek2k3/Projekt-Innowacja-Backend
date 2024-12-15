using Innowacja.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Innowacja.Infrastructure.EF
{
    public class MyDbContext : DbContext
    {
        public DbSet<BrakProduktow> BrakiProduktow { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrakProduktow>(entity =>
            {
<<<<<<< Updated upstream
                entity.HasKey(e => e.IdBraku);
                entity.Property(e => e.NumerPolki).IsRequired();
                entity.Property(e => e.NumerProduktu).IsRequired();
                entity.Property(e => e.Xmin).IsRequired();
                entity.Property(e => e.Xmax).IsRequired();
                entity.Property(e => e.Ymin).IsRequired();
                entity.Property(e => e.Ymax).IsRequired();
                entity.Property(e => e.SciezkaDoPliku).HasMaxLength(int.MaxValue);
=======
                entity.ToTable("Department");
                entity.HasKey(d => d.DepartmentId);
                entity.Property(d => d.DepartmentName).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Shelf>(entity =>
            {
                entity.ToTable("Shelf"); 
                entity.HasKey(s => s.shopShelfId);
                entity.HasOne(s => s.Department)
                      .WithMany(d => d.Shelves)
                      .HasForeignKey(s => s.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductShortage>(entity =>
            {
                entity.ToTable("ProductShortages");
                entity.HasKey(ps => ps.ShortageId);
                entity.Property(ps => ps.ProductName).IsRequired().HasMaxLength(255);
                entity.HasOne(ps => ps.Shelf)
                      .WithMany(s => s.ProductShortages)
                      .HasForeignKey(ps => ps.shopShelfId)
                      .OnDelete(DeleteBehavior.Cascade);
>>>>>>> Stashed changes
            });
        }
    }
}
