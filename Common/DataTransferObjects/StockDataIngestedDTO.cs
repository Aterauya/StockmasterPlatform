using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataTransferObjects
{
    public class StockDataIngestedDTO
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("data")]
        public List<StockIngestedDTO> StockIngested { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
