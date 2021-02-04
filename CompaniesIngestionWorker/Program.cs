using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automatonymous;
using Common.AzureServiceBusClient;
using Common.BusClient;
using CompaniesApi.Interfaces;
using CompaniesEntityFramework.Models;
using CompaniesEntityFramework.Proxies;
using CompaniesIngestionWorker.Helpers;
using Coravel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompaniesIngestionWorker
{
    /// <summary>
    /// The program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            var dateTime = DateTime.Now;
            host.Services
                .UseScheduler(scheduler =>
                {
                    scheduler.OnWorker("SymbolIngestion")
                        .Schedule<CompaniesSymbolIngestionHelper>()
                        .DailyAt(Convert.ToInt16(GetConfiguration().GetSection("IngestionWorkerStartHour").Value),
                            Convert.ToInt16(GetConfiguration().GetSection("IngestionWorkerStartMinute").Value));

                    scheduler.OnWorker("CompanyInformationIngestion")
                        .Schedule<CompanyInformationIngestionHelper>()
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
                    services.AddDbContext<CompanyDbContext>(options => options.UseSqlServer(GetConfiguration().GetConnectionString("CompanyDbConnection")));
                    services.AddScheduler();
                    services.AddTransient<CompaniesSymbolIngestionHelper>();
                    services.AddTransient<CompanyInformationIngestionHelper>();
                    services.AddTransient<ICompanyUrlHelper, CompaniesUrlHelper>();
                    services.AddTransient<ICompanyWriteProxy, CompanyWriteProxy>();
                    services.AddTransient<ICompanyReadProxy, CompanyReadProxy>();
                });
    }
}
