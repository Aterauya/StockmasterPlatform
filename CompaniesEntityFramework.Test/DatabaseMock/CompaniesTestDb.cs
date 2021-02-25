using System;
using System.Collections.Generic;
using System.Text;
using CompaniesEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CompaniesEntityFramework.Test.DatabaseMocks
{
    public class CompaniesTestDb
    {
        private CompanyDbContext _context;

        public CompaniesTestDb(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new CompanyDbContext(options, configuration);
        }

        public void AddCompanies(List<CompanyInformation> companiesInformation)
        {
            _context.AddRange(companiesInformation);
            _context.SaveChanges();
        }

        public void AddCompaniesSymbols(List<CompanySymbol> companySymbols)
        {
            _context.AddRange(companySymbols);
            _context.SaveChanges();
        }

        public CompanyDbContext GetContext()
        {
            return _context;
        }

        public List<CompanySymbol> GetSymbols()
        {
            return new List<CompanySymbol>
            {
                new CompanySymbol
                {
                    SymbolId = new Guid("10f83f75-8d49-454e-8d05-5a4ddf5ec4b7"),
                    Symbol = "Test symbol 1"
                },
                new CompanySymbol
                {
                    SymbolId = new Guid("cdac763f-e755-4938-97b1-246a6caa7a3d"),
                    Symbol = "Test symbol 2"
                },
                new CompanySymbol
                {
                    SymbolId = new Guid("167d38eb-9586-4555-bd55-b556ce588c3b"),
                    Symbol = "Test symbol 3"
                }
            };
        }

        public List<CompanyInformation> GetCompaniesInformation()
        {
            return new List<CompanyInformation>
            {
                new CompanyInformation
                {
                    CompanyId = Guid.Parse("3c1ee777-ac41-456b-9e00-2993668d90d0"),
                    SymbolId = Guid.Parse("10f83f75-8d49-454e-8d05-5a4ddf5ec4b7"),
                    Name = "Test company 1",
                    Exchange = "Test Exchange",
                    Ipo = DateTime.Today,
                    MarketCapitalization = 10,
                    OutstandingShares = 10,
                    Url = "Test url",
                    Logo = "Test logo",
                    CountryName = "Test country name",
                    CurrencyName = "GBP",
                    IndustryName = "Test industry name"
                },
                new CompanyInformation
                {
                    CompanyId = Guid.Parse("abb0784a-cfe2-49f5-80bb-541bb39f5317"),
                    SymbolId = Guid.Parse("10f83f75-8d49-454e-8d05-5a4ddf5ec4b7"),
                    Name = "Test company 2",
                    Exchange = "Test Exchange",
                    Ipo = DateTime.Today,
                    MarketCapitalization = 10,
                    OutstandingShares = 10,
                    Url = "Test url",
                    Logo = "Test logo",
                    CountryName = "Test country name",
                    CurrencyName = "GBP",
                    IndustryName = "Test industry name"
                },
                new CompanyInformation
                {
                    CompanyId = Guid.Parse("8ec5bc71-6722-4f3c-a656-402fbda4de58"),
                    SymbolId = Guid.Parse("10f83f75-8d49-454e-8d05-5a4ddf5ec4b7"),
                    Name = "Test company 3",
                    Exchange = "Test Exchange",
                    Ipo = DateTime.Today,
                    MarketCapitalization = 10,
                    OutstandingShares = 10,
                    Url = "Test url",
                    Logo = "Test logo",
                    CountryName = "Test country name",
                    CurrencyName = "GBP",
                    IndustryName = "Test industry name"
                }
            };
        }
    }
}
