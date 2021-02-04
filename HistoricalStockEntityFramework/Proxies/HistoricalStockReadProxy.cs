using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockApi.Interfaces;
using HistoricalStockEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HistoricalStockEntityFramework.Proxies
{
    /// <summary>
    /// The historical stock read proxy
    /// </summary>
    /// <seealso cref="HistoricalStockApi.Interfaces.IHistoricalStockReadProxy" />
    public class HistoricalStockReadProxy : IHistoricalStockReadProxy
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly HistoricalStockDbContext _dbContext;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HistoricalStockReadProxy> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalStockReadProxy"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public HistoricalStockReadProxy(HistoricalStockDbContext dbContext, ILogger<HistoricalStockReadProxy> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets the historical stocks for company.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>
        /// A list of historical stocks
        /// </returns>
        /// <exception cref="DataException">
        /// </exception>
        public async Task<List<HistoricalStockDto>> GetHistoricalStocksForCompany(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                _logger.LogError("Symbol is empty or null");
                throw new DataException();
            }

            var stocks = await _dbContext.HistoricalStocks
                .Where(hs => hs.StockSymbol.Equals(stockSymbol))
                .Select(s => new HistoricalStockDto
                {
                    HistoricalStockId = s.HistoricalStockId,
                    StockSymbol = s.StockSymbol,
                    OpeningPrice = s.OpeningPrice,
                    HighPrice = s.HighPrice,
                    LowPrice = s.LowPrice,
                    ClosePrice = s.ClosePrice,
                    Volume = s.Volume,
                    ClosingDateTime = s.ClosingDateTime
                }).ToListAsync();

            if (!stocks.Any())
            {
                _logger.LogError("No stocks in the db for that stock symbol");
                throw new DataException();
            }

            return stocks;
        }
    }
}
