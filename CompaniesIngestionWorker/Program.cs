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
    public class Program
    {
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

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }


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
