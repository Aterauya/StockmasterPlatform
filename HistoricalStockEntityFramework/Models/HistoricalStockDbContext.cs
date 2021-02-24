using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HistoricalStockEntityFramework.Models
{
    public class HistoricalStockDbContext : DbContext
    {
        public HistoricalStockDbContext()
        {

        }

        public HistoricalStockDbContext(DbContextOptions<HistoricalStockDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<HistoricalStock> HistoricalStocks { get; set; }
        private readonly IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("HistoricalStockDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricalStock>(entity =>
            {
                entity.HasKey(e => new {e.HistoricalStockId, e.FilterHash})
                    .HasName("HistoricalStock_pk");

                entity.Property(e => e.HistoricalStockId)
                    .ValueGeneratedNever();

                entity.Property(e => e.StockSymbol)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningPrice)
                    .HasColumnType("money");

                entity.Property(e => e.HighPrice)
                    .HasColumnType("money");

                entity.Property(e => e.LowPrice)
                    .HasColumnType("money");

                entity.Property(e => e.ClosePrice)
                    .HasColumnType("money");

                entity.Property(e => e.Volume)
                    .HasColumnType("decimal(20, 10)");

                entity.Property(e => e.ClosingDateTime)
                    .HasColumnType("date");

                entity.Property(e => e.FilterHash)
                    .ValueGeneratedNever()
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasIndex(e => e.FilterHash)
                    .IsUnique();

            });
        }


    }
}
