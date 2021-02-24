using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HistoricalStockApi.DataTransferObjects
{
    /// <summary>
    /// The historical stock data ingested data transfer object
    /// </summary>
    public class HistoricalStockDataIngestedDto
    {
        /// <summary>
        /// Gets or sets the opening price.
        /// </summary>
        /// <value>
        /// The opening price.
        /// </value>
        [JsonProperty("o")]
        public List<double> OpeningPrice { get; set; }

        /// <summary>
        /// Gets or sets the high price.
        /// </summary>
        /// <value>
        /// The high price.
        /// </value>
        [JsonProperty("h")]
        public List<double> HighPrice { get; set; }

        /// <summary>
        /// Gets or sets the low price.
        /// </summary>
        /// <value>
        /// The low price.
        /// </value>
        [JsonProperty("l")]
        public List<double> LowPrice { get; set; }

        /// <summary>
        /// Gets or sets the close price.
        /// </summary>
        /// <value>
        /// The close price.
        /// </value>
        [JsonProperty("c")]
        public List<double> ClosePrice { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        [JsonProperty("v")]
        public List<long> Volume { get; set; }

        /// <summary>
        /// Gets or sets the closing date time.
        /// </summary>
        /// <value>
        /// The closing date time.
        /// </value>
        [JsonProperty("t")]
        public List<long> ClosingDateTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("s")]
        public string Status { get; set; }
    }
}
