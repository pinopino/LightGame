using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Serialization;
using System.Reflection;

namespace LightGame.Silo
{
    public class ClusterHostedService : IHostedService
    {
        private readonly ILogger<ClusterHostedService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHost _silo;

        public ClusterHostedService(ILogger<ClusterHostedService> logger, IConfiguration configuration, ILoggerProvider loggerProvider)
        {
            _logger = logger;
            _configuration = configuration;

            var siloBuilder = Host.CreateDefaultBuilder()
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .UseOrleans((context, builder) =>
                {
                    var config = context.Configuration;
                    builder.Configure<SiloOptions>(options =>
                    {
                        options.SiloName = SiloClusterSetting.SiloName;
                    });
                    builder.ConfigureEndpoints(
                        siloPort: SiloClusterSetting.SiloPort,
                        gatewayPort: SiloClusterSetting.GatewayPort
                    );
                    builder.Configure<HostOptions>(options =>
                    {
                        options.ShutdownTimeout = TimeSpan.FromMinutes(1);
                    });
                    builder.Configure<SchedulingOptions>(options =>
                    {
                        options.AllowCallChainReentrancy = true;
                    });
                    builder.ConfigureServices(services =>
                    {
                        services.AddSerializer(serializerBuilder =>
                        {
                            serializerBuilder.AddNewtonsoftJsonSerializer(type => type.GetCustomAttribute<SerializableAttribute>() is not null);
                        });
                    });
                    builder.UseAdoNetClustering(options =>
                    {
                        options.Invariant = SiloClusterSetting.MembershipInvariant;
                        options.ConnectionString = SiloClusterSetting.MembershipConnStr;
                    });
                    builder.AddAdoNetGrainStorage(StorageNameConst.DefaultStorage, options =>
                    {
                        options.Invariant = SiloClusterSetting.StorageInvariant;
                        options.ConnectionString = SiloClusterSetting.StorageConnStr;
                        options.UseJsonFormat = true;
                    });
                    builder.UseAdoNetReminderService(options =>
                    {
                        options.Invariant = SiloClusterSetting.ReminderInvariant;
                        options.ConnectionString = SiloClusterSetting.ReminderConnStr;
                    });
                    builder.UseDashboard();
                });

            _silo = siloBuilder.Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _silo.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _silo.StopAsync();
        }
    }
}
