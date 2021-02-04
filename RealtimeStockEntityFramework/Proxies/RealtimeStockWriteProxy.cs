using Microsoft.Extensions.DependencyInjection;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockEntityFramework.Models;
using System;
using System.Threading.Tasks;

namespace RealtimeStockEntityFramework.Proxies
{
    /// <summary>
    /// The realtime stock write proxy
    /// </summary>
    /// <seealso cref="RealtimeStockApi.EntityFrameworkInterfaces.IRealtimeStockWriteProxy" />
    public class RealtimeStockWriteProxy : IRealtimeStockWriteProxy
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly RealtimeStockContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealtimeStockWriteProxy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RealtimeStockWriteProxy(RealtimeStockContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the real time stock.
        /// </summary>
        /// <param name="stocks">The stocks.</param>
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
