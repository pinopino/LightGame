using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Serialization;
using System.Net;

namespace LightGame.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseOrleansClient((ctx, clientBuilder) =>
            {
                var config = ctx.Configuration;
                var clusterId = config.GetSection("ClusterConfig")["ClusterId"];
                var serviceId = config.GetSection("ClusterConfig")["ServiceId"];
                var gatewayIp = config.GetSection("ClusterConfig")["GatewayIP"];
                var gatewayPort = config.GetSection("ClusterConfig")["GatewayPort"];

                clientBuilder
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = clusterId;
                        options.ServiceId = serviceId;
                    })
                    .Configure<GatewayOptions>(options =>
                    {
                        options.GatewayListRefreshPeriod = TimeSpan.FromMinutes(10);
                    })
                    .UseStaticClustering(new IPEndPoint(IPAddress.Parse(gatewayIp), int.Parse(gatewayPort)));
            });

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}