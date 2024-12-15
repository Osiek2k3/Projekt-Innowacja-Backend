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
            });
        }
    }

}
