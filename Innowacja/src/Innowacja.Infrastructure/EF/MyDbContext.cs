using Innowacja.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Innowacja.Infrastructure.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Shelf> Shelves { get; set; } = null!;
        public DbSet<ProductShortage> ProductShortages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
<<<<<<< HEAD
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
=======
>>>>>>> 2d8b14fac753cd58416df6795452752f9d5fa8cf
                entity.ToTable("Department");
                entity.HasKey(d => d.DepartmentId);
                entity.Property(d => d.DepartmentName).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Shelf>(entity =>
            {
<<<<<<< HEAD
                entity.ToTable("Shelf"); 
=======
                entity.ToTable("Shelf"); // Wyraźne ustawienie nazwy tabeli
>>>>>>> 2d8b14fac753cd58416df6795452752f9d5fa8cf
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
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> 2d8b14fac753cd58416df6795452752f9d5fa8cf
            });
        }
    }

}
