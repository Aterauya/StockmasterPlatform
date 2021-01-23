using Microsoft.Extensions.DependencyInjection;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockEntityFramework.Models;
using System;
using System.Threading.Tasks;

namespace RealtimeStockEntityFramework.Proxies
{
    public class RealtimeStockWriteProxy : IRealtimeStockWriteProxy
    {
        private readonly RealtimeStockContext _context;

        public RealtimeStockWriteProxy(RealtimeStockContext context)
        {
            _context = context;
        }
        
       public void AddRealTimeStock(StockDataIngestedDTO stocks)
       {
           var today = DateTime.Today;
           foreach (var stock in stocks.StockIngested)
           {
               _context.Add(new RealtimeStock
               {
                   RealtimeStockId = Guid.NewGuid(),
                   DateTimeTraded = today.AddTicks(stock.DateTimeTraded),
                    Price = Convert.ToDecimal(stock.Price),
                   StockSymbol = stock.StockSymbol,
                   Volume = Convert.ToDecimal(stock.Volume)
               });
           }
           _context.SaveChanges();
       }
    }
}
