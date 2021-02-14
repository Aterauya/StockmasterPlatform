using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CompaniesApi.DataTransferObjects
{
    public class CompanyListDto
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company symbol.
        /// </summary>
        /// <value>
        /// The company symbol.
        /// </value>
        public string CompanySymbol { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        /// <value>
        /// The name of the currency.
        /// </value>
        public string CurrencyName { get; set; }
    }
}
