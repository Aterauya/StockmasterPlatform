using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;

namespace CompaniesApi.Interfaces
{
    public interface ICompanyWriteProxy
    {
        Task AddCompanySymbols(List<StockSymbolDTO> stockSymbols);

        Task AddCompanyInformation(List<CompanyInformationDto> companyInformation);
    }
}
