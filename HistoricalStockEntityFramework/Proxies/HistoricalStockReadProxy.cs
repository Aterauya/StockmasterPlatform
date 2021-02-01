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
    public class HistoricalStockReadProxy : IHistoricalStockReadProxy
    {
        private readonly HistoricalStockDbContext _dbContext;
        private readonly ILogger<HistoricalStockReadProxy> _logger;

        public HistoricalStockReadProxy(HistoricalStockDbContext dbContext, ILogger<HistoricalStockReadProxy> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

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
