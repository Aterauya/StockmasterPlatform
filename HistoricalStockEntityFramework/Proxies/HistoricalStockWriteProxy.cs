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
        /// The read proxy
        /// </summary>
        private readonly IHistoricalStockReadProxy _readProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalStockWriteProxy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public HistoricalStockWriteProxy(HistoricalStockDbContext context, ILogger<HistoricalStockWriteProxy> logger,
            IHistoricalStockReadProxy readProxy)
        {
            _context = context;
            _logger = logger;
            _readProxy = readProxy;
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

            var cleanedStocks = await RemoveDuplicateStockEntries(historicalStocks, stockSymbol);

            var stocksToAdd = cleanedStocks
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
                    FilterHash = h.FilterHash
                })
                .ToList();

            if (stocksToAdd.Any())
            {
                await _context.HistoricalStocks.AddRangeAsync();
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<HistoricalStockDto>> RemoveDuplicateStockEntries(List<HistoricalStockDto> stocks, string ticker)
        {
            var copyOfStocks = stocks;

            var currentStocks = await _readProxy.GetHistoricalStocksForCompany(ticker);


            foreach (var stock in currentStocks)
            {
                copyOfStocks.RemoveAll(s => s.FilterHash == stock.FilterHash);
            }

            return copyOfStocks;
        }
    }
}
