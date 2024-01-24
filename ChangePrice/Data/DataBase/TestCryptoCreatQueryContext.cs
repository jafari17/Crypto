using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChangePrice.Data.DataBase
{
    public partial class TestCryptoCreatQueryContext : IdentityDbContext
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
            modelBuilder.ApplyConfiguration(new Configurations.AlertConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CandleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserNameConfiguration());

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
