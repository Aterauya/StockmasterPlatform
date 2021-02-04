using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockEntityFramework.Models;

namespace RealtimeStockEntityFramework.Proxies
{
    /// <summary>
    /// The realtime stock read proxy
    /// </summary>
    /// <seealso cref="RealtimeStockApi.EntityFrameworkInterfaces.IRealtimeStockReadProxy" />
    public class RealtimeStockReadProxy : IRealtimeStockReadProxy
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly RealtimeStockContext _context;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<RealtimeStockReadProxy> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealtimeStockReadProxy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public RealtimeStockReadProxy(RealtimeStockContext context, ILogger<RealtimeStockReadProxy> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets the realtime stocks.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>
        /// A list of realtime stocks
        /// </returns>
        /// <exception cref="DataException"></exception>
        public async Task<List<RealtimeStockDto>> GetRealtimeStocks(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                _logger.LogError("Stock symbol passed in is empty");
                throw new DataException();
            }

            return await _context.RealtimeStock
                .Where(c => c.StockSymbol.Equals(stockSymbol))
                .Select(r => new RealtimeStockDto
                {
                    StockSymbol = r.StockSymbol,
                    Price = r.Price,
                    DateTimeTraded = r.DateTimeTraded,
                    Volume = r.Volume
                })
                .ToListAsync();
        }
    }
}
