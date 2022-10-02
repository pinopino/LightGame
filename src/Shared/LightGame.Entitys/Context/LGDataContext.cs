using Microsoft.EntityFrameworkCore;

namespace LightGame.Entity
{
    public class LGDataContext : DbContext
    {
        public DbSet<GameUser> GameUsers { get; set; }
        public DbSet<ServerConfig> ServerConfigs { get; set; }

        public LGDataContext(DbContextOptions<LGDataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameUser>().ToTable("GameUser");
            modelBuilder.Entity<ServerConfig>().ToTable("ServerConfig");
            modelBuilder.Entity<ServerConfig>().Property(m => m.ApiIP).HasDefaultValue("127.0.0.1");
            modelBuilder.Entity<ServerConfig>().Property(m => m.ApiPort).HasDefaultValue(8000);
            modelBuilder.Entity<ServerConfig>().Property(m => m.GatewayIP).HasDefaultValue("127.0.0.1");
            modelBuilder.Entity<ServerConfig>().Property(m => m.GatewayPort).HasDefaultValue(9001);
            modelBuilder.Entity<ServerConfig>().Property(m => m.LoginIP).HasDefaultValue("127.0.0.1");
            modelBuilder.Entity<ServerConfig>().Property(m => m.LoginPort).HasDefaultValue(8001);
        }
    }
}
