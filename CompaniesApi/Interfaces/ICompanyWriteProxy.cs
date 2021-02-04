using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;

namespace CompaniesApi.Interfaces
{
    /// <summary>
    /// The company write proxy interface
    /// </summary>
    public interface ICompanyWriteProxy
    {
        /// <summary>
        /// Adds the company symbols.
        /// </summary>
        /// <param name="stockSymbols">The stock symbols.</param>
        /// <returns></returns>
        Task AddCompanySymbols(List<StockSymbolDTO> stockSymbols);

        /// <summary>
        /// Adds the company information.
        /// </summary>
        /// <param name="companyInformation">The company information.</param>
        /// <returns></returns>
        Task AddCompanyInformation(CompanyInformationDto companyInformation);
    }
}
