using CompaniesApi.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace CompaniesIngestionWorker.Helpers
{
    /// <summary>
    /// The companies url helper
    /// </summary>
    /// <seealso cref="CompaniesApi.Interfaces.ICompanyUrlHelper" />
    public class CompaniesUrlHelper : ICompanyUrlHelper
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesUrlHelper"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CompaniesUrlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the finnhub companies symbol URL.
        /// </summary>
        /// <returns>
        /// The companies symbol url
        /// </returns>
        public string GetFinnhubCompaniesSymbolUrl()
        {
            return string.Format(_configuration["finnHubCompaniesSymbolUrl"], _configuration["finnHubCompaniesExchange"], _configuration["finnHubToken"]);
        }

        /// <summary>
        /// Gets the finnhub company profile URL.
        /// </summary>
        /// <param name="ticker">The ticker.</param>
        /// <returns>
        /// The companies profile url
        /// </returns>
        public string GetFinnhubCompanyProfileUrl(string ticker)
        {
            return string.Format(_configuration["finnHubCompaniesProfileUrl"], ticker, _configuration["finnHubToken"]);
        }
    }
}
