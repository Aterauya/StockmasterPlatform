using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using CompaniesEntityFramework.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompaniesEntityFramework.Proxies
{
    public class CompanyWriteProxy : ICompanyWriteProxy
    {
        private readonly CompanyDbContext _dbContext;
        private readonly ILogger<CompanyWriteProxy> _logger;

        public CompanyWriteProxy(CompanyDbContext dbContext, ILogger<CompanyWriteProxy> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddCompanySymbols(List<StockSymbolDTO> stockSymbols)
        {
            if (!stockSymbols.Any())
            {

                throw new DataException();
            }

            var existingSymbols = _dbContext.CompanySymbol.ToList();
            var copyOfStockSymbols = new List<StockSymbolDTO>();
            copyOfStockSymbols.AddRange(stockSymbols);

            foreach (var symbol in stockSymbols)
            {
                foreach (var existingSymbol in existingSymbols)
                {
                    if (existingSymbol.Symbol.Equals(symbol.Symbol))
                    {
                        copyOfStockSymbols.Remove(symbol);
                    }
                }
            }

            if (copyOfStockSymbols.Any())
            {
                var symbols = copyOfStockSymbols
                    .Select(s => new CompanySymbol
                    {
                        SymbolId = Guid.NewGuid(),
                        Symbol = s.Symbol
                    }).ToList();

                await _dbContext.AddRangeAsync(symbols);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddCompanyInformation(CompanyInformationDto companyInformation)
        {
            if (companyInformation != null && !string.IsNullOrEmpty(companyInformation.Name))
            {
                var existingInformation = _dbContext.CompanyInformation.ToList();

                var existsInDb = false;

                foreach (var eInformation in existingInformation)
                {
                    if (eInformation.Name.Equals(companyInformation.Name))
                    {
                        existsInDb = true;
                    }
                }

                if (!existsInDb)
                {
                    var information = new CompanyInformation
                    {
                        CompanyId = Guid.NewGuid(),
                        SymbolId = companyInformation.SymbolId,
                        Name = companyInformation.Name,
                        Exchange = companyInformation.Exchange,
                        Ipo = companyInformation.Ipo ?? new DateTime(),
                        MarketCapitalization = companyInformation.MarketCapitalization,
                        Url = companyInformation.Url,
                        OutstandingShares = companyInformation.OutstandingShares,
                        Logo = companyInformation.Logo,
                        CountryName = companyInformation.CountryName,
                        CurrencyName = companyInformation.CurrencyName,
                        IndustryName = companyInformation.IndustryName
                    };

                    await _dbContext.AddAsync(information);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
