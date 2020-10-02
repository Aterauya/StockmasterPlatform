using Common.AzureServiceBusClient;
using Common.BusClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtimeStockApi;
using RealtimeStockApi.Interfaces;
using RealtimeStockIngestion.Helpers;

namespace RealtimeStockIngestion
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


            var serviceProvider = new ServiceCollection()
            .AddSingleton<IBusClient, AzureServiceBusClient>()
            .AddTransient<IRealtimeStockIngestion, RealtimeIngestionHelper>()
            .AddTransient<IRealtimeStockUrlHelper, RealtimeStockUrlHelper>()
            .AddSingleton(_configuration)
            .BuildServiceProvider();

            var realtimeStockIngestion = serviceProvider.GetService<IRealtimeStockIngestion>();
            realtimeStockIngestion.StartIngestion();
        }
    }
}
