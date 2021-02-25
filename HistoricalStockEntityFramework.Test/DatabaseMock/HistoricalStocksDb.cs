using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HistoricalStockEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace HistoricalStockEntityFramework.Test.DatabaseMock
{
    public class HistoricalStocksDb
    {
        private HistoricalStockDbContext _context;

        public HistoricalStocksDb(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<HistoricalStockDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new HistoricalStockDbContext(options, configuration);
        }

        public async Task AddHistoricalStocks(List<HistoricalStock> stocks)
        {
            await _context.AddRangeAsync(stocks);
            await _context.SaveChangesAsync();
        }

        public HistoricalStockDbContext GetContext()
        {
            return _context;
        }


        public List<HistoricalStock> GetHistoricalStocks()
        {
            return new List<HistoricalStock>
            {
                new HistoricalStock
                {
                    HistoricalStockId = Guid.NewGuid(),
                    ClosePrice = 2.00,
                    OpeningPrice = 10.00,
                    HighPrice = 15.00,
                    LowPrice = 1.00,
                    StockSymbol = "Fake",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    FilterHash = $"Fake{DateTime.Today}"
                },
                new HistoricalStock
                {
                    HistoricalStockId = Guid.NewGuid(),
                    ClosePrice = 5.00,
                    OpeningPrice = 10.00,
                    HighPrice = 7.00,
                    LowPrice = 1.00,
                    StockSymbol = "Fake",
                    Volume = 20,
                    ClosingDateTime = DateTime.Today,
                    FilterHash = $"Fake{DateTime.Today}"

                },
                new HistoricalStock
                {
                    HistoricalStockId = Guid.NewGuid(),
                    ClosePrice = 2.00,
                    OpeningPrice = 10.00,
                    HighPrice = 15.00,
                    LowPrice = 1.00,
                    StockSymbol = "Fake",
                    Volume = 40,
                    ClosingDateTime = DateTime.Today,
                    FilterHash = $"Fake{DateTime.Today}"
                }
            };
        }
    }
}
