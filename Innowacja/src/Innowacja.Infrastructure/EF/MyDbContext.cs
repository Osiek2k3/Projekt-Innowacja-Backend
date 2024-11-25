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
                entity.HasKey(e => e.IdBraku);
                entity.Property(e => e.NumerPolki).IsRequired();
                entity.Property(e => e.NumerProduktu).IsRequired();
                entity.Property(e => e.Xmin).IsRequired();
                entity.Property(e => e.Xmax).IsRequired();
                entity.Property(e => e.Ymin).IsRequired();
                entity.Property(e => e.Ymax).IsRequired();
                entity.Property(e => e.SciezkaDoPliku).HasMaxLength(int.MaxValue);
            });
        }
    }
}
