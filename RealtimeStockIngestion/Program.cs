using Common.AzureServiceBusClient;
using Common.BusClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtimeStockApi;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockApi.Interfaces;
using RealtimeStockEntityFramework.Models;
using RealtimeStockEntityFramework.Proxies;
using RealtimeStockIngestion.Helpers;

namespace RealtimeStockIngestion
{
    /// <summary>
    /// The program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


            var serviceProvider = new ServiceCollection()
            .AddSingleton<RealtimeStockContext>()
            .AddTransient<IRealtimeStockWriteProxy, RealtimeStockWriteProxy>()
            .AddTransient<IRealtimeStockIngestion, RealtimeIngestionHelper>()
            .AddTransient<IRealtimeStockUrlHelper, RealtimeStockUrlHelper>()
            .AddSingleton(_configuration)


            .BuildServiceProvider();

            var realtimeStockIngestion = serviceProvider.GetService<IRealtimeStockIngestion>();
            realtimeStockIngestion.StartIngestion();
        }
    }
}
