using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.Interfaces
{
    /// <summary>
    /// The Company Url Helper interface
    /// </summary>
    public interface ICompanyUrlHelper
    {
        /// <summary>
        /// Gets the finnhub companies symbol URL.
        /// </summary>
        /// <returns>The companies symbol url</returns>
        string GetFinnhubCompaniesSymbolUrl();

        /// <summary>
        /// Gets the finnhub company profile URL.
        /// </summary>
        /// <param name="ticker">The ticker.</param>
        /// <returns>The companies profile url</returns>
        string GetFinnhubCompanyProfileUrl(string ticker);
    }
}
