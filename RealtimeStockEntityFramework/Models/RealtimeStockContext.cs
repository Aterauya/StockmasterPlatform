using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RealtimeStockEntityFramework.Models
{
    public partial class RealtimeStockContext : DbContext
    {
        public RealtimeStockContext()
        {
        }

        public RealtimeStockContext(DbContextOptions<RealtimeStockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RealtimeStock> RealtimeStock { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-E1IC06C;Database=RealtimeStock;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RealtimeStock>(entity =>
            {
                entity.Property(e => e.RealtimeStockId).ValueGeneratedNever();

                entity.Property(e => e.DateTimeTraded).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StockSymbol)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Volume).HasColumnType("decimal(20, 10)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
