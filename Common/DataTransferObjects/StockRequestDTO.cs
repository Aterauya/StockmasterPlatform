using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataTransferObjects
{
    public class StockRequestDTO
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
