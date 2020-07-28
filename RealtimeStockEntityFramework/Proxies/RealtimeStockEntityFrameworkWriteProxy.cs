using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockEntityFramework.Models;
using System;
using System.Threading.Tasks;

namespace RealtimeStockEntityFramework.Contexts
{
    public class RealtimeStockEntityFrameworkWriteProxy : IRealtimeStockEmntityFrameworkWriteProxy
    {
        private readonly RealtimeStockContext _context;

        public RealtimeStockEntityFrameworkWriteProxy(RealtimeStockContext context)
        {
            _context = context;
        }

       public Task AddRealTimeStock(StockDataIngestedDTO stock)
        {
            using(var context = _context)
            {
                context.Add(new RealtimeStock
                {
                    RealtimeStockId = Guid.NewGuid(),
                    DateTimeTraded = new DateTime(stock.StockIngested[0].DateTimeTraded),
                    Price = Convert.ToDecimal(stock.StockIngested[0].Price),
                    StockSymbol = stock.StockIngested[0].StockSymbol,
                    Volume = Convert.ToDecimal(stock.StockIngested[0].Volume)
                });
                return context.SaveChangesAsync();
            }
        }
    }
}
