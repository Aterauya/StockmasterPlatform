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
    /// <summary>
    /// The company read proxy
    /// </summary>
    /// <seealso cref="CompaniesApi.Interfaces.ICompanyReadProxy" />
    public class CompanyReadProxy : ICompanyReadProxy
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly CompanyDbContext _dbContext;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CompanyReadProxy> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyReadProxy"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public CompanyReadProxy(CompanyDbContext dbContext, ILogger<CompanyReadProxy> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets the company symbols.
        /// </summary>
        /// <returns>
        /// The companies stock symbols
        /// </returns>
        /// <exception cref="DataException"></exception>
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

        /// <summary>
        /// Gets all company information.
        /// </summary>
        /// <returns>
        /// All of the companies information
        /// </returns>
        /// <exception cref="DataException"></exception>
        public async Task<List<CompanyListDto>> GetAllCompanyInformation()
        {
            if (!_dbContext.CompanyInformation.Any())
            {
                _logger.LogError("Error when getting companies information from database");
                throw new DataException();
            }

            var information = await _dbContext
                .CompanyInformation
                .Include(s => s.Symbol)
                .Select(ci => new CompanyListDto
                {
                    CompanyId = ci.CompanyId,
                    Name = ci.Name,
                    CompanySymbol = ci.Symbol.Symbol,
                    CurrencyName = ci.CurrencyName
                })
                .OrderBy(e => e.CompanySymbol)
                .ToListAsync();

            return information;
        }

        /// <summary>
        /// Gets the company information.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>
        /// The specific companies information
        /// </returns>
        /// <exception cref="DataException"></exception>
        public async Task<CompanyInformationDto> GetCompanyInformation(Guid companyId)
        {
            if (!_dbContext.CompanyInformation.Any() || companyId == null)
            {
                _logger.LogError("Error when getting company information from database");
                throw new DataException();
            }

            var company = await _dbContext
                .CompanyInformation
                .Where(ci => ci.CompanyId == companyId)
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
                }).FirstOrDefaultAsync();

            return company;
        }

        /// <summary>
        /// Gets the company list.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The company list</returns>
        /// <exception cref="DataException"></exception>
        public async Task<CompanyPageListDto> GetCompanyList(int startIndex, int endIndex, int pageNumber)
        {
            if (!_dbContext.CompanyInformation.Any())
            {
                _logger.LogError("Error when getting company information from database");
                throw new DataException();
            }

            var count = _dbContext.CompanyInformation.Count();

            var data = await _dbContext.CompanyInformation
                .Skip(startIndex)
                .Take(startIndex - endIndex)
                .Select(ci => new CompanyListDto
                {
                    CompanyId = ci.CompanyId,
                    Name = ci.Name,
                    CompanySymbol = ci.Symbol.Symbol,
                    CurrencyName = ci.CurrencyName,
                }).ToListAsync();

            return new CompanyPageListDto
            {
                Data = data,
                CurrentPageNumber = pageNumber,
                DataCount = count,
                HasNext = endIndex / 10 > 0,
                HasPrevious = startIndex / 10 > 0,
            };
        }
    }
}
