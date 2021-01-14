using Microsoft.Extensions.DependencyInjection;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockEntityFramework.Models;
using System;
using System.Threading.Tasks;

namespace RealtimeStockEntityFramework.Proxies
{
    public class RealtimeStockEntityFrameworkWriteProxy : IRealtimeStockEntityFrameworkWriteProxy
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RealtimeStockEntityFrameworkWriteProxy(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
       public Task AddRealTimeStock(StockDataIngestedDTO stock)
       {
           using (var scope = _serviceScopeFactory.CreateScope())
           using (var context = scope.ServiceProvider.GetService<RealtimeStockContext>())
           {
                var today = DateTime.Today;
                var todayTime = today.AddTicks(stock.StockIngested[0].DateTimeTraded);
                context.Add(new RealtimeStock
                {
                    RealtimeStockId = stock.Id,
                    DateTimeTraded = DateTime.Now,
                    Price = Convert.ToDecimal(stock.StockIngested[0].Price),
                    StockSymbol = stock.StockIngested[0].StockSymbol,
                    Volume = Convert.ToDecimal(stock.StockIngested[0].Volume)
                });
                return context.SaveChangesAsync();
            }
        }
    }
}
