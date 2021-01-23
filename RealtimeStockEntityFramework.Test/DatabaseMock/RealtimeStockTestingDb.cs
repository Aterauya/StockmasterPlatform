using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RealtimeStockEntityFramework.Models;

namespace RealtimeStockEntityFramework.Test.DatabaseMock
{
    public class RealtimeStockTestingDb
    {
        private RealtimeStockContext _context;

        public RealtimeStockTestingDb(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<RealtimeStockContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new RealtimeStockContext(options, configuration);
        }

        public void AddRealtimeStock(List<RealtimeStock> realtimeStock)
        {
            _context.AddRange(realtimeStock);
            _context.SaveChanges();
        }

        public RealtimeStockContext GetContext()
        {
            return _context;
        }

        public List<RealtimeStock> GetRealtimeStocks()
        {
            return new List<RealtimeStock>
            {
                new RealtimeStock
                {
                    RealtimeStockId = Guid.NewGuid(),
                    Price = Convert.ToDecimal(2.00),
                    StockSymbol = "Stock 1",
                    DateTimeTraded = DateTime.Today,
                    Volume = Convert.ToDecimal(5)
                },
                new RealtimeStock
                {
                    RealtimeStockId = Guid.NewGuid(),
                    Price = Convert.ToDecimal(3.00),
                    StockSymbol = "Stock 1",
                    DateTimeTraded = DateTime.Today,
                    Volume = Convert.ToDecimal(5)
                },
                new RealtimeStock
                {
                    RealtimeStockId = Guid.NewGuid(),
                    Price = Convert.ToDecimal(5.00),
                    StockSymbol = "Stock 1",
                    DateTimeTraded = DateTime.Today,
                    Volume = Convert.ToDecimal(5)
                }
            };
        }
    }
}
