using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HistoricalStockApi.DataTransferObjects
{
    /// <summary>
    /// The stock symbol data transfer object
    /// </summary>
    public class StockSymbolsDto
    {
        /// <summary>
        /// Gets or sets the stock identifier.
        /// </summary>
        /// <value>
        /// The stock identifier.
        /// </value>
        [JsonProperty("symbolId")]
        public Guid StockId { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        [JsonProperty("symbol")]
        public string StockSymbol { get; set; }
    }
}
