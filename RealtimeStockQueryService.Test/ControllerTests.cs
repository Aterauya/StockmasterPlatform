using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockEntityFramework.Proxies;
using RealtimeStockQueryService.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealtimeStockApi.EntityFrameworkInterfaces;

namespace RealtimeStockQueryService.Test
{
    public class Tests
    {
        #region TestData

        private List<RealtimeStockDto> GetRealtimeStocks()
        {
            return new List<RealtimeStockDto>
            {
                new RealtimeStockDto
                {
                    StockSymbol = "Test stock symbol 1",
                    Price = Convert.ToDecimal(2.00),
                    DateTimeTraded = DateTime.Today,
                    Volume = Convert.ToDecimal(10.5)
                },
                new RealtimeStockDto
                {
                    StockSymbol = "Test stock symbol 1",
                    Price = Convert.ToDecimal(3.00),
                    DateTimeTraded = DateTime.Today,
                    Volume = Convert.ToDecimal(200.1)
                },
                new RealtimeStockDto
                {
                    StockSymbol = "Test stock symbol 1",
                    Price = Convert.ToDecimal(3.10),
                    DateTimeTraded = DateTime.Today,
                    Volume = Convert.ToDecimal(560)
                }
            };
        }

        #endregion
        private static ILogger<RealtimeStockController> _logger;
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<RealtimeStockController>();
        }

        [Test]
        public async Task GetAllRealtimeStocksReturnsData()
        {
            // Arrange
            var readProxy = new Mock<IRealtimeStockReadProxy>();
            readProxy.Setup(proxy => proxy.GetRealtimeStocks(It.IsAny<string>()))
                .ReturnsAsync(GetRealtimeStocks());

            var controller = new RealtimeStockController(readProxy.Object, _logger);

            // Act
            var response = await controller.GetRealtimeStock("Test stock symbol 1");

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<RealtimeStockDto>>), response);
            Assert.AreEqual(Convert.ToDecimal(2.00), response.Value[0].Price);
            Assert.AreEqual(Convert.ToDecimal(3.00), response.Value[1].Price);
            Assert.AreEqual(Convert.ToDecimal(3.10), response.Value[2].Price);
        }

        [Test]
        public async Task GetAllRealtimeStocksReturnsNotFound()
        {
            // Arrange
            var readProxy = new Mock<IRealtimeStockReadProxy>();
            readProxy.Setup(proxy => proxy.GetRealtimeStocks(It.IsAny<string>()))
                .ReturnsAsync(new List<RealtimeStockDto>());

            var controller = new RealtimeStockController(readProxy.Object, _logger);

            // Act
            var response = await controller.GetRealtimeStock("");

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<RealtimeStockDto>>), response);
            Assert.IsInstanceOf(typeof(NotFoundResult), response.Result);
        }
    }
}