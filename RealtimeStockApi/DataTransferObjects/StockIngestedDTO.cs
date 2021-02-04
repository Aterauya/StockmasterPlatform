using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtimeStockApi.DataTransferObjects
{
    /// <summary>
    /// Stock ingested data transfer object
    /// </summary>
    public class StockIngestedDTO
    {
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("p")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        [JsonProperty("s")]
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the date time traded.
        /// </summary>
        /// <value>
        /// The date time traded.
        /// </value>
        [JsonProperty("t")]
        public long DateTimeTraded { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        [JsonProperty("v")]
        public double Volume { get; set; }
    }
}
