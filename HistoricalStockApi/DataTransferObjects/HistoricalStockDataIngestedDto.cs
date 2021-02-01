using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HistoricalStockApi.DataTransferObjects
{
    public class HistoricalStockDataIngestedDto
    {
        [JsonProperty("o")]
        public List<double> OpeningPrice { get; set; }

        [JsonProperty("h")]
        public List<double> HighPrice { get; set; }

        [JsonProperty("l")]
        public List<double> LowPrice { get; set; }

        [JsonProperty("c")]
        public List<double> ClosePrice { get; set; }

        [JsonProperty("v")]
        public List<int> Volume { get; set; }

        [JsonProperty("t")]
        public List<long> ClosingDateTime { get; set; }

        [JsonProperty("s")]
        public string Status { get; set; }
    }
}
