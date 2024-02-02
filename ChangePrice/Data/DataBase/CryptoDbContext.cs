
using ChangePrice.Model_DataBase;
using ChangePrice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;


namespace ChangePrice.Data.DataBase
{
    public partial class CryptoDbContext : IdentityDbContext
    {
        public CryptoDbContext()
        {
        }

        public CryptoDbContext(DbContextOptions<CryptoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alert> Alert { get; set; }
        public virtual DbSet<AlertAuto> AlertAuto { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new Configurations.AlertConfiguration());

        //    OnModelCreatingPartial(modelBuilder);
        //    base.OnModelCreating(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
