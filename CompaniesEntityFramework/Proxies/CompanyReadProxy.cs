using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using CompaniesEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompaniesEntityFramework.Proxies
{
    public class CompanyReadProxy : ICompanyReadProxy
    {
        private readonly CompanyDbContext _dbContext;
        private readonly ILogger<CompanyReadProxy> _logger;

        public CompanyReadProxy(CompanyDbContext dbContext, ILogger<CompanyReadProxy> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<StockSymbolDTO>> GetCompanySymbols()
        {
            if (!_dbContext.CompanySymbol.Any())
            {
                _logger.LogError("Error when getting companies symbols from database");
                throw new DataException();
            }

            return await _dbContext
                .CompanySymbol
                .Select(cs => new StockSymbolDTO
                {
                    SymbolId = cs.SymbolId,
                    Symbol = cs.Symbol
                })
                .OrderBy(cs => cs.Symbol)
                .ToListAsync();
        }

        public async Task<List<CompanyInformationDto>> GetAllCompanyInformation()
        {
            if (!_dbContext.CompanyInformation.Any())
            {
                _logger.LogError("Error when getting companies information from database");
                throw new DataException();
            }

            var information = await _dbContext
                .CompanyInformation
                .Include(s => s.Symbol)
                .Select(ci => new CompanyInformationDto
                {
                    CompanyId = ci.CompanyId,
                    SymbolId = ci.SymbolId,
                    CompanySymbol = ci.Symbol.Symbol,
                    Name = ci.Name,
                    Exchange = ci.Exchange,
                    Ipo = ci.Ipo,
                    MarketCapitalization = ci.MarketCapitalization,
                    OutstandingShares = ci.OutstandingShares,
                    Url = ci.Url,
                    Logo = ci.Logo,
                    CountryName = ci.CountryName,
                    CurrencyName = ci.CurrencyName,
                    IndustryName = ci.IndustryName
                }).ToListAsync();

            return information;
        }

        public async Task<CompanyInformationDto> GetCompanyInformation(Guid companyId)
        {
            if (!_dbContext.CompanyInformation.Any() || companyId == null)
            {
                _logger.LogError("Error when getting company information from database");
                throw new DataException();
            }

            return await _dbContext
                .CompanyInformation
                .Where(ci => ci.CompanyId == companyId)
                .Select(ci => new CompanyInformationDto
                {
                    CompanyId = ci.CompanyId,
                    SymbolId = ci.SymbolId,
                    Name = ci.Name,
                    Exchange = ci.Exchange,
                    Ipo = ci.Ipo,
                    MarketCapitalization = ci.MarketCapitalization,
                    OutstandingShares = ci.OutstandingShares,
                    Url = ci.Url,
                    Logo = ci.Logo,
                    CountryName = ci.CountryName,
                    CurrencyName = ci.CurrencyName,
                    IndustryName = ci.IndustryName
                }).FirstOrDefaultAsync();
        }
    }
}
