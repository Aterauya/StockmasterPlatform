using System;
using System.Data;
using System.Threading.Tasks;
using CompaniesEntityFramework.Models;
using CompaniesEntityFramework.Proxies;
using CompaniesEntityFramework.Test.DatabaseMocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CompaniesEntityFramework.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
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

            var testDb = new TestDb();

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

            var testDb = new TestDb();


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

            var testDb = new TestDb();

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

            var testDb = new TestDb();


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

            var testDb = new TestDb();

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

            var testDb = new TestDb();


            var readProxy = new CompanyReadProxy(testDb.GetContext(), logger);

            // Assert
            var ex = Assert.ThrowsAsync<DataException>(async () => await readProxy.GetCompanyInformation(new Guid("3c1ee777-ac41-456b-9e00-2993668d90d0")));
            Assert.That(ex.Message, Is.EqualTo("Data Exception."));
        }
    }
}