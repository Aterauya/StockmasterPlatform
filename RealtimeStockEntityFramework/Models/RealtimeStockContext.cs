using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace RealtimeStockEntityFramework.Models
{
    public partial class RealtimeStockContext : DbContext
    {
        public RealtimeStockContext()
        {
        }

        public RealtimeStockContext(DbContextOptions options, IConfiguration configuration)
            :base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<RealtimeStock> RealtimeStock { get; set; }
        private readonly IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RealtimeStockDbConnection"));
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
