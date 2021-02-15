using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using CompaniesQueryService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CompaniesQueryService.Test
{
    public class Tests
    {
        #region TestDatas

        private List<CompanyListDto> GetCompaniesInformation()
        {
            return new List<CompanyListDto>
            {
                new CompanyListDto
                {
                    CompanyId = new Guid("ddd0e31f-a120-4b7d-a5ba-ef62f4d7309e"),
                    Name = "Test Company 1",
                    CurrencyName = "Test Company 1 Currency",
                },
                new CompanyListDto
                {
                    CompanyId = new Guid("1c3d5ce9-16eb-4eab-a0f7-f1808d447f29"),
                    Name = "Test Company 2",
                    CurrencyName = "Test Company 2 Currency",
                }
            };
        }

        private List<StockSymbolDTO> GetStockSymbols()
        {
            return new List<StockSymbolDTO>
            {
                new StockSymbolDTO
                {
                    SymbolId = new Guid("18c4f615-6c2a-490c-b839-1f2515036288"),
                    Symbol = "Test symbol 1"
                },
                new StockSymbolDTO
                {
                    SymbolId = new Guid("0443be49-c725-4090-84c0-b3ed37bf993c"),
                    Symbol = "Test symbol 2"
                },
                new StockSymbolDTO
                {
                    SymbolId = new Guid("64f7773e-5230-4fdd-9e20-321a3a81754a"),
                    Symbol = "Test symbol 3"
                }
            };
        }

        private CompanyInformationDto GetCompanyInformation()
        {
            return new CompanyInformationDto
            {
                CompanyId = new Guid("ddd0e31f-a120-4b7d-a5ba-ef62f4d7309e"),
                SymbolId = new Guid("69a264ce-c801-44a9-8b80-83af2a324dfe"),
                Name = "Test Company 1",
                Exchange = "Test Company Exchange 1",
                Ipo = DateTime.Now,
                MarketCapitalization = 2.00M,
                OutstandingShares = 100,
                Url = "Test Company Url 1",
                Logo = "Test Company Logo 1",
                CountryName = "Test Company 1 Country",
                CurrencyName = "Test Company 1 Currency",
                IndustryName = "Test Company 1 Industry"
            };
        }

        #endregion

        private static ILogger<CompaniesController> logger;
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            logger = factory.CreateLogger<CompaniesController>();
        }

        [Test]
        public async Task GetAllCompaniesInformationReturnsData()
        {
            // Arrange
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetAllCompanyInformation())
                .ReturnsAsync(GetCompaniesInformation());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetCompaniesInformation();

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<CompanyInformationDto>>), result);
            Assert.AreEqual("Test Company 1", result.Value[0].Name);
            Assert.AreEqual("Test Company 2", result.Value[1].Name);
        }

        [Test]
        public async Task GetAllCompaniesReturnsNotFound()
        {
            // Arrange 
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetAllCompanyInformation())
                .ReturnsAsync(new List<CompanyListDto>());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetCompaniesInformation();

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<CompanyInformationDto>>), result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }

        [Test]
        public async Task GetCompanyReturnsData()
        {
            // Arrange 
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetCompanyInformation(It.IsAny<Guid>()))
                .ReturnsAsync(GetCompanyInformation());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetCompanyInformation(Guid.Parse("ddd0e31f-a120-4b7d-a5ba-ef62f4d7309e"));

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<CompanyInformationDto>), result);
            Assert.AreEqual("Test Company 1", result.Value.Name);
        }

        [Test]
        public async Task GetCompanyReturnsNotFoundFromEmptyGuid()
        {
            // Arrange 
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetCompanyInformation(It.IsAny<Guid>()))
                .ReturnsAsync(new CompanyInformationDto());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetCompanyInformation(Guid.Empty);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<CompanyInformationDto>), result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }

        [Test]
        public async Task GetCompanyReturnsNotFoundFromNoCompanies()
        {
            // Arrange 
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetCompanyInformation(It.IsAny<Guid>()))
                .ReturnsAsync(new CompanyInformationDto());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetCompanyInformation(Guid.Parse("ddd0e31f-a120-4b7d-a5ba-ef62f4d7309e"));

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<CompanyInformationDto>), result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }

        [Test]
        public async Task GetStockSymbolsReturnsData()
        {
            // Arrange 
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetCompanySymbols())
                .ReturnsAsync(GetStockSymbols());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetAllCompanySymbols();

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<StockSymbolDTO>>), result);
            Assert.AreEqual("Test symbol 1", result.Value[0].Symbol);
        }

        [Test]
        public async Task GetStockSymbolReturnsNotFound()
        {
            // Arrange 
            var mockProxy = new Mock<ICompanyReadProxy>();
            mockProxy.Setup(proxy => proxy.GetCompanySymbols())
                .ReturnsAsync(new List<StockSymbolDTO>());

            var controller = new CompaniesController(mockProxy.Object, logger);

            // Act
            var result = await controller.GetAllCompanySymbols();

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<StockSymbolDTO>>), result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }
    }
}