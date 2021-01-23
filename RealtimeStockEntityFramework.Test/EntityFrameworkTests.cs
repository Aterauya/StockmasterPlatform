using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockEntityFramework.Models;
using RealtimeStockEntityFramework.Proxies;
using RealtimeStockEntityFramework.Test.DatabaseMock;

namespace RealtimeStockEntityFramework.Test
{
    public class Tests
    {
        private static Mock<IConfiguration> mockConfiguration;

        [SetUp]
        public void Setup()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "testDB")]).Returns("mock value");

            mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);
        }

        private static DbSet<RealtimeStock> GetQueryableCompanyInformationMockDbSet(
            List<RealtimeStock> sourceList)
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<RealtimeStock>>();
            dbSet.As<IQueryable<RealtimeStock>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<RealtimeStock>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<RealtimeStock>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<RealtimeStock>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<RealtimeStock>())).Callback<RealtimeStock>(s => sourceList.Add(s));
            return dbSet.Object;
        }

        [Test]
        public async Task GetRealtimeStock_ReturnsData()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<RealtimeStockReadProxy>();

            var testDb = new RealtimeStockTestingDb(mockConfiguration.Object);

            testDb.AddRealtimeStock(testDb.GetRealtimeStocks());

            var readProxy = new RealtimeStockReadProxy(testDb.GetContext(), logger);

            // Act
            var result = await readProxy.GetRealtimeStocks("Stock 1");

            // Assert
            Assert.AreEqual(Convert.ToDecimal(2.00), result[0].Price);
            Assert.AreEqual(Convert.ToDecimal(3.00), result[1].Price);
            Assert.AreEqual(Convert.ToDecimal(5.00), result[2].Price);
        }

        [Test]
        public async Task GetRealtimeStocks_ThrowsDataException()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<RealtimeStockReadProxy>();

            var testDb = new RealtimeStockTestingDb(mockConfiguration.Object);

            testDb.AddRealtimeStock(testDb.GetRealtimeStocks());

            var readProxy = new RealtimeStockReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetRealtimeStocks(""));
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }

        [Test]
        public void AddRealtimeStocks_AddsData()
        {
            // Arrange
            var mockContext = new Mock<RealtimeStockContext>();
            var realtimeStocksInDb = new List<RealtimeStock>();

            mockContext.Setup(r => r.RealtimeStock)
                .Returns(GetQueryableCompanyInformationMockDbSet(realtimeStocksInDb));
            
            var writeProxy = new RealtimeStockWriteProxy(mockContext.Object);

            var realtimeStocks = new StockDataIngestedDTO
            {
                StockIngested = new List<StockIngestedDTO>
                    {
                        new StockIngestedDTO
                        {
                            Price = 2.00,
                            StockSymbol = "Stock 1",
                            DateTimeTraded = 500,
                            Volume = 3.00
                        },
                        new StockIngestedDTO
                        {
                            Price = 2.00,
                            StockSymbol = "Stock 2",
                            DateTimeTraded = 500,
                            Volume = 5.00
                        }
                    }
            };

            // Act
            writeProxy.AddRealTimeStock(realtimeStocks);

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}