using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task AddCompanyInformation(List<CompanyInformationDto> companyInformation)
        {
            if (companyInformation.Any())
            {
                var existingInformation = _dbContext.CompanyInformation.ToList();
                var copyOfCompanyInformation = new List<CompanyInformationDto>();
                copyOfCompanyInformation.AddRange(companyInformation);

                foreach (var cIinformation in companyInformation)
                {
                    foreach (var eInformation in existingInformation)
                    {
                        if (eInformation.SymbolId.Equals(cIinformation.SymbolId))
                        {
                            copyOfCompanyInformation.Remove(cIinformation);
                        }
                    }
                }

                if (copyOfCompanyInformation.Any())
                {
                    var information = copyOfCompanyInformation
                        .Select(s => new CompanyInformation
                        {
                            CompanyId = Guid.NewGuid(),
                            SymbolId = s.SymbolId,
                            Name = s.Name,
                            Exchange = s.Exchange,
                            Ipo = s.Ipo ?? new DateTime(),
                            MarketCapitalization = s.MarketCapitalization,
                            Url = s.Url,
                            OutstandingShares = s.OutstandingShares,
                            Logo = s.Logo,
                            CountryName = s.CountryName,
                            CurrencyName = s.CurrencyName,
                            IndustryName = s.IndustryName

                        }).ToList();

                    await _dbContext.AddRangeAsync(information);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
