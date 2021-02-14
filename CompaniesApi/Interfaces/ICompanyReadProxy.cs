using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;

namespace CompaniesApi.Interfaces
{
    /// <summary>
    /// The Company Read Proxy interface
    /// </summary>
    public interface ICompanyReadProxy
    {
        /// <summary>
        /// Gets the company symbols.
        /// </summary>
        /// <returns>The companies stock symbols</returns>
        Task<List<StockSymbolDTO>> GetCompanySymbols();

        /// <summary>
        /// Gets all company information.
        /// </summary>
        /// <returns>All of the companies information</returns>
        Task<List<CompanyListDto>> GetAllCompanyInformation();

        /// <summary>
        /// Gets the company information.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>The specific companies information</returns>
        Task<CompanyInformationDto> GetCompanyInformation(Guid companyId);
    }
}
