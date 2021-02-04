using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtimeStockApi.DataTransferObjects
{
    /// <summary>
    /// Stock data ingested data transfer object
    /// </summary>
    public class StockDataIngestedDTO
    {
        /// <summary>
        /// Gets or sets the stock ingested.
        /// </summary>
        /// <value>
        /// The stock ingested.
        /// </value>
        [JsonProperty("data")]
        public List<StockIngestedDTO> StockIngested { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
