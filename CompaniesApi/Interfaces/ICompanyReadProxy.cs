using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;

namespace CompaniesApi.Interfaces
{
    public interface ICompanyReadProxy
    {
        Task<List<StockSymbolDTO>> GetCompanySymbols();

        Task<List<CompanyInformationDto>> GetCompanyInformation();

        Task<CompanyInformationDto> GetCompanyInformation(Guid companyId);
    }
}
