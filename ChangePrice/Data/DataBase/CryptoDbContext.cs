
using ChangePrice.ModelDataBase;
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
        //    //modelBuilder.Seed();
        //    //base.OnModelCreating(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
//public static class ModelBuilderExtensions
//{
//    public static void Seed(this ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Author>().HasData(
//            new Author
//            {
//                AuthorId = 1,
//                FirstName = "William",
//                LastName = "Shakespeare"
//            }
//        );
//        modelBuilder.Entity<Book>().HasData(
//            new Book { BookId = 1, AuthorId = 1, Title = "Hamlet" },
//            new Book { BookId = 2, AuthorId = 1, Title = "King Lear" },
//            new Book { BookId = 3, AuthorId = 1, Title = "Othello" }
//        );
//    }
//}