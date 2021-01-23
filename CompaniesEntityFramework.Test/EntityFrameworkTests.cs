using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesEntityFramework.Models;
using CompaniesEntityFramework.Proxies;
using CompaniesEntityFramework.Test.DatabaseMocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;

namespace CompaniesEntityFramework.Test
{
    public class Tests
    {
        private static Mock<IConfiguration> mockConfiguration;

        private static DbSet<CompanySymbol> GetQueryableCompanySymbolMockDbSet(List<CompanySymbol> sourceList)
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<CompanySymbol>>();
            dbSet.As<IQueryable<CompanySymbol>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<CompanySymbol>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<CompanySymbol>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<CompanySymbol>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<CompanySymbol>())).Callback<CompanySymbol>(s => sourceList.Add(s));
            return dbSet.Object;
        }

        private static DbSet<CompanyInformation> GetQueryableCompanyInformationMockDbSet(
            List<CompanyInformation> sourceList)
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<CompanyInformation>>();
            dbSet.As<IQueryable<CompanyInformation>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<CompanyInformation>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<CompanyInformation>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<CompanyInformation>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<CompanyInformation>())).Callback<CompanyInformation>(s => sourceList.Add(s));
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
        public async Task GetSymbols_ReturnsData()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyReadProxy>();

            var testDb = new CompaniesTestDb(mockConfiguration.Object);

            testDb.AddCompaniesSymbols(testDb.GetSymbols());

            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Act
            var result = await readProxy.GetCompanySymbols();

            // Assert
            Assert.AreEqual("Test symbol 1", result[0].Symbol);
            Assert.AreEqual("Test symbol 2", result[1].Symbol);
            Assert.AreEqual("Test symbol 3", result[2].Symbol);
        }

        [Test]
        public void GetSymbols_ThrowsDataException()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyReadProxy>();

            var testDb = new CompaniesTestDb(mockConfiguration.Object);


            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetCompanySymbols());
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }

        [Test]
        public async Task GetAllCompanyInformation_ReturnsData()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyReadProxy>();

            var testDb = new CompaniesTestDb(mockConfiguration.Object);

            testDb.AddCompanies(testDb.GetCompaniesInformation());

            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Act
            var result = await readProxy.GetAllCompanyInformation();

            // Assert
            Assert.AreEqual("Test company 1", result[0].Name);
            Assert.AreEqual("Test company 2", result[1].Name);
            Assert.AreEqual("Test company 3", result[2].Name);
        }

        [Test]
        public void GetAllCompanyInformation_ThrowsDataException()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyReadProxy>();

            var testDb = new CompaniesTestDb(mockConfiguration.Object);


            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetAllCompanyInformation());
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }

        [Test]
        public async Task GetCompanyInformation_ReturnsData()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyReadProxy>();

            var testDb = new CompaniesTestDb(mockConfiguration.Object);

            testDb.AddCompanies(testDb.GetCompaniesInformation());

            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Act
            var result = await readProxy.GetCompanyInformation(new Guid("3c1ee777-ac41-456b-9e00-2993668d90d0"));

            // Assert
            Assert.AreEqual("Test company 1", result.Name);
        }

        [Test]
        public void GetCompanyInformation_ThrowsDataException()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyReadProxy>();

            var testDb = new CompaniesTestDb(mockConfiguration.Object);


            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetCompanyInformation(new Guid("3c1ee777-ac41-456b-9e00-2993668d90d0")));
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }

        [Test]
        public async Task AddCompanySymbols_GivenDataNotInDb_AddsData()
        {
            // Arrange
            var mockContext = new Mock<CompanyDbContext>();


            var companySymbolsInDb = new List<CompanySymbol>();

            mockContext.Setup(c => c.CompanySymbol)
                .Returns(GetQueryableCompanySymbolMockDbSet(companySymbolsInDb));

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyWriteProxy>();

            var writeProxy = new CompanyWriteProxy(mockContext.Object, logger);

            var companySymbols = new List<StockSymbolDTO>
            {
                new StockSymbolDTO
                {
                    SymbolId = Guid.NewGuid(),
                    Symbol = "Stock symbol 1"
                },
                new StockSymbolDTO
                {
                    SymbolId = Guid.NewGuid(),
                    Symbol = "Stock symbol 2"
                }
            };

            // Act
            await writeProxy.AddCompanySymbols(companySymbols);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task AddCompanySymbols_GivenSymbolsAlreadyInDb_AddsNothing()
        {
            // Arrange
            var mockContext = new Mock<CompanyDbContext>();


            var companySymbolsInDb = new List<CompanySymbol>
            {
                new CompanySymbol
                {
                    SymbolId = Guid.NewGuid(),
                    Symbol = "Stock symbol 1"
                },
                new CompanySymbol
                {
                    SymbolId = Guid.NewGuid(),
                    Symbol = "Stock symbol 2"
                }

            };

            mockContext.Setup(c => c.CompanySymbol)
            .Returns(GetQueryableCompanySymbolMockDbSet(companySymbolsInDb));

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyWriteProxy>();

            var writeProxy = new CompanyWriteProxy(mockContext.Object, logger);

            var companySymbols = new List<StockSymbolDTO>
            {
                new StockSymbolDTO
                {
                    SymbolId = Guid.NewGuid(),
                    Symbol = "Stock symbol 1"
                },
                new StockSymbolDTO
                {
                    SymbolId = Guid.NewGuid(),
                    Symbol = "Stock symbol 2"
                }
            };

            // Act
            await writeProxy.AddCompanySymbols(companySymbols);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task AddCompanyInformation_GivenDataNotInDb_AddsDataToDb()
        {
            // Arrange
            var mockContext = new Mock<CompanyDbContext>();


            var companyInformationsInDb = new List<CompanyInformation>();

            mockContext.Setup(c => c.CompanyInformation)
                .Returns(GetQueryableCompanyInformationMockDbSet(companyInformationsInDb));

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyWriteProxy>();

            var writeProxy = new CompanyWriteProxy(mockContext.Object, logger);

            var companyInformation = new List<CompanyInformationDto>
            {
                new CompanyInformationDto()
                {
                    CompanyId = Guid.NewGuid(),
                    Name = "Company name 1"
                },
                new CompanyInformationDto
                {
                    CompanyId = Guid.NewGuid(),
                    Name = "Company name 2"
                }
            };

            // Act
            await writeProxy.AddCompanyInformation(companyInformation);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task AddCompanyInformation_GivenDataInDb_AddsNoDataToDb()
        {
            // Arrange
            var mockContext = new Mock<CompanyDbContext>();


            var companyInformationsInDb = new List<CompanyInformation>()
            {
                new CompanyInformation()
                {
                    CompanyId = Guid.NewGuid(),
                    Name = "Company name 1"
                },
                new CompanyInformation
                {
                    CompanyId = Guid.NewGuid(),
                    Name = "Company name 2"
                }
            };

            mockContext.Setup(c => c.CompanyInformation)
                .Returns(GetQueryableCompanyInformationMockDbSet(companyInformationsInDb));

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<CompanyWriteProxy>();

            var writeProxy = new CompanyWriteProxy(mockContext.Object, logger);

            var companyInformation = new List<CompanyInformationDto>
            {
                new CompanyInformationDto
                {
                    CompanyId = Guid.NewGuid(),
                    Name = "Company name 1"
                },
                new CompanyInformationDto
                {
                    CompanyId = Guid.NewGuid(),
                    Name = "Company name 2"
                }
            };

            // Act
            await writeProxy.AddCompanyInformation(companyInformation);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}