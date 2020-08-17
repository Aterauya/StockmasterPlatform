using Common.AzureServiceBusClient;
using Common.BusClient;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtimeStockApi;
using RealtimeStockIngestion.Helpers;
using System;
using System.IO;

namespace RealtimeStockIngestion
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IBusClient, AzureServiceBusClient>()
            .AddTransient<IRealtimeStockIngestion, RealtimeIngestionHelper>()
            .AddTransient<IUrlHelper, UrlHelper>()
            .BuildServiceProvider();

            var realtimeStockIngestion = serviceProvider.GetService<IRealtimeStockIngestion>();
            realtimeStockIngestion.StartIngestion();
        }
    }
}
