using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.AzureServiceBusClient;
using Common.BusClient;
using CompaniesApi.Interfaces;
using CompaniesIngestionWorker.Helpers;
using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompaniesIngestionWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Services
                .UseScheduler(scheduler => 
                {
                    scheduler
                        .Schedule<CompaniesIngestionHelper>()
                        .EverySecond();
                });
                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IBusClient, AzureServiceBusClient>();
                    services.AddScheduler();
                    services.AddTransient<CompaniesIngestionHelper>();
                    services.AddTransient<ICompanyUrlHelper, CompaniesUrlHelper>();

                });
    }
}
