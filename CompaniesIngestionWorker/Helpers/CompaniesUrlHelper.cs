using CompaniesApi.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace CompaniesIngestionWorker.Helpers
{
    public class CompaniesUrlHelper : ICompanyUrlHelper
    {
        private readonly IConfiguration _configuration;
        public CompaniesUrlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetFinnhubCompaniesSymbolUrl()
        {
            return string.Format(_configuration["finnHubCompaniesSymbolUrl"], _configuration["finnHubCompaniesExchange"], _configuration["finnHubToken"]);
        }

        public string GetFinnhubCompanyProfileUrl(string ticker)
        {
            return string.Format(_configuration["finnHubCompaniesProfileUrl"], ticker, _configuration["finnHubToken"]);
        }
    }
}
