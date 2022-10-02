using Microsoft.EntityFrameworkCore;

namespace LightGame.Entity
{
    public class LGRecordContext : DbContext
    {
        public DbSet<LoginRecord> LoginRecords { get; set; }

        public LGRecordContext(DbContextOptions<LGRecordContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoginRecord>().ToTable("LoginRecord");
        }
    }
}
