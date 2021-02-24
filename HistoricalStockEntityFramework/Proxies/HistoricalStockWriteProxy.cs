using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockApi.Interfaces;
using HistoricalStockEntityFramework.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace HistoricalStockEntityFramework.Proxies
{
    /// <summary>
    /// The historical stock write proxy
    /// </summary>
    /// <seealso cref="HistoricalStockApi.Interfaces.IHistoricalStockWriteProxy" />
    public class HistoricalStockWriteProxy : IHistoricalStockWriteProxy
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly HistoricalStockDbContext _context;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HistoricalStockWriteProxy> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalStockWriteProxy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HistoricalStockWriteProxy(HistoricalStockDbContext context, ILogger<HistoricalStockWriteProxy> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds the historical stock.
        /// </summary>
        /// <param name="historicalStocks">The historical stocks.</param>
        /// <param name="stockSymbol">The stock symbol</param>
        /// <exception cref="DataException"></exception>
        public async Task AddHistoricalStock(List<HistoricalStockDto> historicalStocks, string stockSymbol)
        {
            if (!historicalStocks.Any())
            {
                _logger.LogError("List of historical stocks is empty");
                throw new DataException();
            }


            var stocksToAdd = historicalStocks
                .Select(h => new HistoricalStock
                {
                    HistoricalStockId = Guid.NewGuid(),
                    StockSymbol = h.StockSymbol,
                    OpeningPrice = h.OpeningPrice,
                    HighPrice = h.HighPrice,
                    LowPrice = h.LowPrice,
                    ClosePrice = h.ClosePrice,
                    Volume = h.Volume,
                    ClosingDateTime = h.ClosingDateTime,
                    FilterHash = $"{h.StockSymbol}{h.ClosingDateTime.Date}"
                })
                .ToList();

            
            try
            {
                await _context.HistoricalStocks.AddRangeAsync(await RemoveDuplicateStockEntries(stocksToAdd, stockSymbol));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e.Message);
            }
        }

        public async Task<List<HistoricalStock>> RemoveDuplicateStockEntries(List<HistoricalStock> stocks, string ticker)
        {
            var copyOfStocks = stocks;

            var currentStocks = await _context.HistoricalStocks
                .Where(s => s.StockSymbol.Equals(ticker))
                .ToListAsync();


            foreach (var stock in currentStocks)
            {
                copyOfStocks.RemoveAll(s => s.FilterHash == stock.FilterHash);
            }

            return copyOfStocks;
        }
    }
}
