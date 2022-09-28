using Microsoft.Extensions.Configuration;

namespace LightGame.Silo
{
    public class SiloClusterSetting
    {
        static SiloClusterSetting()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(ProcessDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string ProcessDirectory
        {
            get
            {
#if NETSTANDARD2_0 || NETCOREAPP3_1_OR_GREATER || NET5_0_OR_GREATER
                return AppContext.BaseDirectory;
#else
                return AppDomain.CurrentDomain.BaseDirectory;
#endif
            }
        }

        public static IConfigurationRoot Configuration { get; }

        public static string ClusterId => Configuration.GetSection("Cluster:ClusterId").Value;

        public static string ServiceId => Configuration.GetSection("Cluster:ServiceId").Value;

        public static string SiloName => Configuration.GetSection("Cluster:SiloName").Value;

        public static int SiloPort => int.Parse(Configuration.GetSection("Cluster:SiloPort").Value);

        public static int GatewayPort => int.Parse(Configuration.GetSection("Cluster:GatewayPort").Value);

        public static int PrimarySiloPort => Configuration.GetValue<int>("Cluster:PrimarySiloPort", -1);

        public static string MembershipInvariant => Configuration.GetSection("Cluster:MembershipConfig:Invariant").Value;

        public static string MembershipConnStr => Configuration.GetSection("Cluster:MembershipConfig:ConnectionString").Value;

        public static string StorageInvariant => Configuration.GetSection("Cluster:StorageConfig:Invariant").Value;

        public static string StorageConnStr => Configuration.GetSection("Cluster:StorageConfig:ConnectionString").Value;
        
        public static string ReminderInvariant => Configuration.GetSection("Cluster:ReminderConfig:Invariant").Value;

        public static string ReminderConnStr => Configuration.GetSection("Cluster:ReminderConfig:ConnectionString").Value;
    }
}
