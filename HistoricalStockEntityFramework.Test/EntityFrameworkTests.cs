using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockEntityFramework.Models;
using HistoricalStockEntityFramework.Proxies;
using HistoricalStockEntityFramework.Test.DatabaseMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace HistoricalStockEntityFramework.Test
{
    public class Tests
    {
        private static Mock<IConfiguration> mockConfiguration;

        private static DbSet<HistoricalStock> GetQueryableHistoricalStockDbSet(List<HistoricalStock> sourceList)
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<HistoricalStock>>();
            dbSet.As<IQueryable<HistoricalStock>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<HistoricalStock>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<HistoricalStock>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<HistoricalStock>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<HistoricalStock>())).Callback<HistoricalStock>(s => sourceList.Add(s));
            return dbSet.Object;
        }

        [SetUp]
        public void Setup()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "testDB")]).Returns("mock value");

            mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);
        }

        [Test]
        public async Task GetHistoricalStocksForCompany_WithCorrectSymbol_ReturnsData()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HistoricalStockReadProxy>();

            var testDb = new HistoricalStocksDb(mockConfiguration.Object);

            await testDb.AddHistoricalStocks(testDb.GetHistoricalStocks());

            var readProxy = new HistoricalStockReadProxy(testDb.GetContext(), logger);

            // Act
            var result = await readProxy.GetHistoricalStocksForCompany("Fake");

            // Assert
            Assert.AreEqual(10, result[0].Volume);
            Assert.AreEqual(20, result[1].Volume);
            Assert.AreEqual(40, result[2].Volume);
        }

        [Test]
        public async Task GetHistoricalStocksForCompany_WithIncorrectSymbol_ThrowsDataException()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HistoricalStockReadProxy>();

            var testDb = new HistoricalStocksDb(mockConfiguration.Object);

            await testDb.AddHistoricalStocks(testDb.GetHistoricalStocks());

            var readProxy = new HistoricalStockReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetHistoricalStocksForCompany(""));
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }

        [Test]
        public void GetHistoricalStocksForCompany_WithNoData_ThrowsDataException()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HistoricalStockReadProxy>();

            var testDb = new HistoricalStocksDb(mockConfiguration.Object);

            var readProxy = new HistoricalStockReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetHistoricalStocksForCompany(""));
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }

        [Test]
        public async Task AddHistoricalStock_RecordDoesntAlreadyExist_AddsData()
        {
            // Arrange
            var mockContext = new Mock<HistoricalStockDbContext>();

            var stocksInDb = new List<HistoricalStock>();

            mockContext.Setup(c => c.HistoricalStocks)
                .Returns(GetQueryableHistoricalStockDbSet(stocksInDb));

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HistoricalStockWriteProxy>();

            var writeProxy = new HistoricalStockWriteProxy(mockContext.Object, logger);

            var stocks = new List<HistoricalStockDto>
            {
                new HistoricalStockDto
                {
                    StockSymbol = "Fake1",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    ClosePrice = 9,
                    HighPrice = 11,
                    HistoricalStockId = Guid.NewGuid(),
                    LowPrice = 1,
                    OpeningPrice = 5,
                },
                new HistoricalStockDto
                {
                    StockSymbol = "Fake2",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    ClosePrice = 9,
                    HighPrice = 11,
                    HistoricalStockId = Guid.NewGuid(),
                    LowPrice = 1,
                    OpeningPrice = 5,
                }
            };

            // Act
            await writeProxy.AddHistoricalStock(stocks);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task AddHistoricalStock_RecordAlreadyExists_DoesntAddData()
        {
            // Arrange
            var mockContext = new Mock<HistoricalStockDbContext>();

            var stocksInDb = new List<HistoricalStock>
            {
                new HistoricalStock
                {
                    StockSymbol = "Fake1",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    ClosePrice = 9,
                    HighPrice = 11,
                    HistoricalStockId = Guid.NewGuid(),
                    LowPrice = 1,
                    OpeningPrice = 5,
                    FilterHash = $"Fake1{DateTime.Today}"

                },
                new HistoricalStock
                {
                    StockSymbol = "Fake2",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    ClosePrice = 9,
                    HighPrice = 11,
                    HistoricalStockId = Guid.NewGuid(),
                    LowPrice = 1,
                    OpeningPrice = 5,
                    FilterHash = $"Fake2{DateTime.Today}"
                }
            };

            mockContext.Setup(c => c.HistoricalStocks)
                .Returns(GetQueryableHistoricalStockDbSet(stocksInDb));

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HistoricalStockWriteProxy>();

            var writeProxy = new HistoricalStockWriteProxy(mockContext.Object, logger);

            var stocks = new List<HistoricalStockDto>
            {
                new HistoricalStockDto
                {
                    StockSymbol = "Fake1",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    ClosePrice = 9,
                    HighPrice = 11,
                    HistoricalStockId = Guid.NewGuid(),
                    LowPrice = 1,
                    OpeningPrice = 5,
                },
                new HistoricalStockDto
                {
                    StockSymbol = "Fake2",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today,
                    ClosePrice = 9,
                    HighPrice = 11,
                    HistoricalStockId = Guid.NewGuid(),
                    LowPrice = 1,
                    OpeningPrice = 5,
                }
            };

            // Act
            await writeProxy.AddHistoricalStock(stocks);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}