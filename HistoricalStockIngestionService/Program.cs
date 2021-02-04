using System;
using Coravel;
using HistoricalStockApi;
using HistoricalStockApi.Interfaces;
using HistoricalStockEntityFramework.Models;
using HistoricalStockEntityFramework.Proxies;
using HistoricalStockIngestionService.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HistoricalStockIngestionService
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
            IHost host = CreateHostBuilder(args).Build();
            host.Services
                .UseScheduler(scheduler =>
                {
                    scheduler.Schedule<HistoricalStockIngestionHelper>()
                        .DailyAt(Convert.ToInt16(GetConfiguration().GetSection("IngestionWorkerStartHour").Value), 
                            Convert.ToInt16(GetConfiguration().GetSection("IngestionWorkerStartMinute").Value));
                });
            host.Run();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScheduler();
                    services.AddTransient<HistoricalStockIngestionHelper>();
                    services.AddTransient<IHistoricalStockUrlHelper, HistoricalStockUrlHelper>();
                    services.AddSingleton(GetConfiguration());
                    services.AddTransient<IHistoricalStockWriteProxy, HistoricalStockWriteProxy>();
                    services.AddDbContext<HistoricalStockDbContext>();
                });
    }
}
