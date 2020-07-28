using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtimeStockApi.DataTransferObjects
{
    public class StockIngestedDTO
    {
        [JsonProperty("p")]
        public double Price { get; set; }

        [JsonProperty("s")]
        public string StockSymbol { get; set; }

        [JsonProperty("t")]
        public long DateTimeTraded { get; set; }

        [JsonProperty("v")]
        public double Volume { get; set; }
    }
}
