using System;
using Newtonsoft.Json;

namespace RealtimeStockApi.DataTransferObjects
{
    /// <summary>
    /// Stock symbol data transfer object
    /// </summary>
    public class StockSymbolsDTO
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
