﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockApi.Interfaces;
using HistoricalStockEntityFramework.Models;
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
        /// <exception cref="DataException"></exception>
        public async Task AddHistoricalStock(List<HistoricalStockDto> historicalStocks)
        {
            if (!historicalStocks.Any())
            {
                _logger.LogError("List of historical stocks is empty");
                throw new DataException();
            }

            var stocksToAdd = historicalStocks
                .Where(StockDoesNotExist)
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
                    FilterHash = $"{h.StockSymbol}{h.ClosingDateTime}"
                })
                .ToList();

            if (stocksToAdd.Any())
            {
                await _context.HistoricalStocks.AddRangeAsync(stocksToAdd);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Stocks the does not exist.
        /// </summary>
        /// <param name="historicalStock">The historical stock.</param>
        /// <returns></returns>
        private bool StockDoesNotExist(HistoricalStockDto historicalStock)
        {
            return !_context.HistoricalStocks
                .Any(c => c.FilterHash.Equals($"{historicalStock.StockSymbol}{historicalStock.ClosingDateTime}"));

        }
    }
}
