﻿// <auto-generated />
using System;
using HistoricalStockEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HistoricalStockEntityFramework.Migrations
{
    [DbContext(typeof(HistoricalStockDbContext))]
    partial class HistoricalStockDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HistoricalStockEntityFramework.Models.HistoricalStock", b =>
                {
                    b.Property<Guid>("HistoricalStockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilterHash")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("money");

                    b.Property<DateTime>("ClosingDateTime")
                        .HasColumnType("date");

                    b.Property<decimal>("HighPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("LowPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("OpeningPrice")
                        .HasColumnType("money");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(20, 10)");

                    b.HasKey("HistoricalStockId", "FilterHash")
                        .HasName("HistoricalStock_pk");

                    b.HasIndex("FilterHash")
                        .IsUnique();

                    b.ToTable("HistoricalStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
