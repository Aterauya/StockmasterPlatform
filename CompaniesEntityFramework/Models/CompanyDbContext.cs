using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompaniesEntityFramework.Models
{
    public partial class CompanyDbContext : DbContext
    {
        public CompanyDbContext()
        {

        }
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompanyInformation> CompanyInformation { get; set; }
        public virtual DbSet<CompanySymbol> CompanySymbol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-E1IC06C;Database=CompanyDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CompanyInformation>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("CompanyInformation_pk");

                entity.Property(e => e.CompanyId).ValueGeneratedNever();

                entity.Property(e => e.Exchange)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Ipo).HasColumnType("date");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MarketCapitalization).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IndustryName)
                    .IsRequired()
                    .HasMaxLength(200);

                

                entity.HasOne(d => d.Symbol)
                    .WithMany(p => p.CompanyInformation)
                    .HasForeignKey(d => d.SymbolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CompanySymbol_CompanyInformation");
            });

            modelBuilder.Entity<CompanySymbol>(entity =>
            {
                entity.HasKey(e => e.SymbolId)
                    .HasName("CompanySymbol_pk");

                entity.Property(e => e.SymbolId).ValueGeneratedNever();

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
