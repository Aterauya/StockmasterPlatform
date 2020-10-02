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

        public string GetFinnhubCompaniesUrl()
        {
            return string.Format(_configuration["finnHubCompaniesUrl"], _configuration["finnHubCompaniesExchange"], _configuration["finnHubToken"]);
        }
    }
}
