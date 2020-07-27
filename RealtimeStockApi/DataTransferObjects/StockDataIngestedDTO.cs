using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtimeStockApi.DataTransferObjects
{
    public class StockDataIngestedDTO
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("data")]
        public StockIngestedDTO StockIngested { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
