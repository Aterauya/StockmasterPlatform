using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using CompaniesEntityFramework.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CompaniesEntityFramework.Proxies
{
    public class CompanyWriteProxy : ICompanyWriteProxy
    {
        private readonly CompanyDbContext _dbContext;
        public CompanyWriteProxy(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCompanySymbols(List<StockSymbolDTO> stockSymbols)
        {
            if (stockSymbols.Any())
            {
                var existingSymbols = _dbContext.CompanySymbol.ToList();
                List<StockSymbolDTO> copyOfStockSymbols = new List<StockSymbolDTO>();
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
