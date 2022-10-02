using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LightGame.Entity
{
    /// <summary>
    /// Add-Migration InitLGData -c LGDataContext -o Migrations/LGData
    /// Update-Database -c LGDataContext
    /// Remove-Migration -c LGDataContext
    /// </summary>
    public class LGDataContextFactory : IDesignTimeDbContextFactory<LGDataContext>
    {
        private MySqlServerVersion _serverVersion = new MySqlServerVersion(new Version(5, 7));

        public LGDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LGDataContext>();
            optionsBuilder.UseMySql(ConfigHelper.GetConnectionString("LGData"), _serverVersion);

            return new LGDataContext(optionsBuilder.Options);
        }
    }

    /// <summary>
    /// Add-Migration InitLGRecord -c LGRecordContext -o Migrations/LGRecord
    /// Update-Database -c LGRecordContext
    /// Remove-Migration -c LGRecordContext
    /// </summary>
    public class LGRecordContextFactory : IDesignTimeDbContextFactory<LGRecordContext>
    {
        private MySqlServerVersion _serverVersion = new MySqlServerVersion(new Version(5, 7));

        public LGRecordContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LGRecordContext>();
            optionsBuilder.UseMySql(ConfigHelper.GetConnectionString("LGRecord"), _serverVersion);

            return new LGRecordContext(optionsBuilder.Options);
        }
    }
}
