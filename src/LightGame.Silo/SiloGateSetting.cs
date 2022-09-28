using Microsoft.Extensions.Configuration;

namespace LightGame.Silo
{
    public class SiloGateSetting
    {
        static SiloGateSetting()
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

        public static bool UseLibuv
        {
            get
            {
                var libuv = Configuration.GetSection("Gate:Libuv").Value;
                return !string.IsNullOrEmpty(libuv) && bool.Parse(libuv);
            }
        }

        public static bool UseSsl
        {
            get
            {
                var ssl = Configuration.GetSection("Gate:SSL").Value;
                return !string.IsNullOrEmpty(ssl) && bool.Parse(ssl);
            }
        }

        public static string pfxFile => Configuration.GetSection("Gate:pfxFile").Value;

        public static string pfxPwd => Configuration.GetSection("Gate:pfxPwd").Value;

        public static bool UseWebsocket => bool.Parse(Configuration.GetSection("Gate:Websocket").Value);

        public static string IP => Configuration.GetSection("Gate:IP").Value;

        public static int Port => int.Parse(Configuration.GetSection("Gate:Port").Value);
    }
}
