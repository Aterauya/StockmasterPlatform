using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CompaniesApi.DataTransferObjects
{
    /// <summary>
    /// The CompanyInformation Data transfer object
    /// </summary>
    public class CompanyInformationDto
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the symbol identifier.
        /// </summary>
        /// <value>
        /// The symbol identifier.
        /// </value>
        public Guid SymbolId { get; set; }

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
        /// Gets or sets the exchange.
        /// </summary>
        /// <value>
        /// The exchange.
        /// </value>
        public string Exchange { get; set; }

        /// <summary>
        /// Gets or sets the ipo.
        /// </summary>
        /// <value>
        /// The ipo.
        /// </value>
        public DateTime? Ipo { get; set; }

        /// <summary>
        /// Gets or sets the market capitalization.
        /// </summary>
        /// <value>
        /// The market capitalization.
        /// </value>
        public decimal MarketCapitalization { get; set; }

        /// <summary>
        /// Gets or sets the outstanding shares.
        /// </summary>
        /// <value>
        /// The outstanding shares.
        /// </value>
        [JsonProperty("shareOutstanding")]
        public float OutstandingShares { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("weburl")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        /// <value>
        /// The logo.
        /// </value>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        [JsonProperty("country")]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        /// <value>
        /// The name of the currency.
        /// </value>
        [JsonProperty("currency")]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the industry.
        /// </summary>
        /// <value>
        /// The name of the industry.
        /// </value>
        [JsonProperty("finnhubIndustry")]
        public string IndustryName { get; set; }
    }
}
