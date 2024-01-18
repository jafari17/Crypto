﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChangePrice.DataBase
{
    public partial class TestCryptoCreatQueryContext : DbContext
    {
        public TestCryptoCreatQueryContext()
        {
        }

        public TestCryptoCreatQueryContext(DbContextOptions<TestCryptoCreatQueryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alert> Alert { get; set; }
        public virtual DbSet<Candle> Candle { get; set; }
        public virtual DbSet<UserName> UserName { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.DateRegisterTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastTouchPrice).HasColumnType("datetime");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.PriceDifference).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Alert)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Alert__UserID__08B54D69");
            });

            modelBuilder.Entity<Candle>(entity =>
            {
                entity.Property(e => e.CandleId).HasColumnName("CandleID");

                entity.Property(e => e.ClosePrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.HighPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LowPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OpenPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<UserName>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserName__1788CCAC1FDDD4C7");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.EmailAddress).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}