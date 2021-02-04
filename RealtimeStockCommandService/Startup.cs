using Common.AzureServiceBusClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealtimeStockApi.EntityFrameworkInterfaces;
using Common.BusClient;
using RealtimeStockCommandService.MessageHandlers;
using Microsoft.Extensions.Configuration;
using RealtimeStockEntityFramework.Proxies;
using RealtimeStockEntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace RealtimeStockCommandService
{
    /// <summary>
    /// The startup class
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RealtimeStockContext>(options => options.UseSqlServer(_configuration.GetConnectionString("RealtimeStockDbConnection")));
            services.AddTransient<IBusMessageHandler, RealtimeStockMessageHandler>();
            services.AddTransient<IBusMessageConsumer, AzureServiceBusMessageConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var consumer = app.ApplicationServices.GetRequiredService<IBusMessageConsumer>();
            consumer.RegisterConsumer();

        }
    }
}
