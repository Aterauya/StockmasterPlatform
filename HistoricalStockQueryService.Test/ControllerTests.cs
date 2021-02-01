using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockApi.Interfaces;
using HistoricalStockQueryService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HistoricalStockQueryService.Test
{
    public class Tests
    {
        private static ILogger<HistoricalStocksController> logger;

        #region TestData

        private static List<HistoricalStockDto> GetHistoricalStocks()
        {
            return new List<HistoricalStockDto>
            {
                new HistoricalStockDto
                {
                    HistoricalStockId = Guid.NewGuid(),
                    ClosePrice = 2.00,
                    OpeningPrice = 10.00,
                    HighPrice = 15.00,
                    LowPrice = 1.00,
                    StockSymbol = "Fake",
                    Volume = 10,
                    ClosingDateTime = DateTime.Today
                },
                new HistoricalStockDto
                {
                    HistoricalStockId = Guid.NewGuid(),
                    ClosePrice = 5.00,
                    OpeningPrice = 10.00,
                    HighPrice = 7.00,
                    LowPrice = 1.00,
                    StockSymbol = "Fake",
                    Volume = 20,
                    ClosingDateTime = DateTime.Today
                },
                new HistoricalStockDto
                {
                    HistoricalStockId = Guid.NewGuid(),
                    ClosePrice = 2.00,
                    OpeningPrice = 10.00,
                    HighPrice = 15.00,
                    LowPrice = 1.00,
                    StockSymbol = "Fake",
                    Volume = 40,
                    ClosingDateTime = DateTime.Today
                }
            };
        }

        #endregion


        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            logger = factory.CreateLogger<HistoricalStocksController>();
        }

        [Test]
        public async Task GetHistoricalStocks_WhenPassedCorrectSymbol_ReturnsData()
        {
            // Arrange
            var mockProxy = new Mock<IHistoricalStockReadProxy>();
            mockProxy.Setup(proxy => proxy.GetHistoricalStocksForCompany(It.IsAny<string>()))
                .ReturnsAsync(GetHistoricalStocks());

            var controller = new HistoricalStocksController(mockProxy.Object, logger);
            // Act
            var result = await controller.GetHistoricalStocks("Fake");

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<HistoricalStockDto>>), result);
            Assert.AreEqual(10, result.Value[0].Volume);
            Assert.AreEqual(20, result.Value[1].Volume);
        }

        [Test]
        public async Task GetHistoricalStocks_WhenPassedEmptyString_Returns404()
        {
            // Arrange
            var mockProxy = new Mock<IHistoricalStockReadProxy>();
            mockProxy.Setup(proxy => proxy.GetHistoricalStocksForCompany(It.IsAny<string>()))
                .ReturnsAsync(GetHistoricalStocks());

            var controller = new HistoricalStocksController(mockProxy.Object, logger);
            // Act
            var result = await controller.GetHistoricalStocks("");

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<HistoricalStockDto>>), result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);

        }

        [Test]
        public async Task GetHistoricalStocks_WhenPassedSymbolWithNoStocks_Returns404()
        {
            // Arrange
            var mockProxy = new Mock<IHistoricalStockReadProxy>();
            mockProxy.Setup(proxy => proxy.GetHistoricalStocksForCompany(It.IsAny<string>()))
                .ReturnsAsync(new List<HistoricalStockDto>());

            var controller = new HistoricalStocksController(mockProxy.Object, logger);
            // Act
            var result = await controller.GetHistoricalStocks("");

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<HistoricalStockDto>>), result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }
    }
}