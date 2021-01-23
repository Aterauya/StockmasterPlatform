using System;
using Newtonsoft.Json;

namespace RealtimeStockApi.DataTransferObjects
{
    public class StockSymbolsDTO
    {
        [JsonProperty("symbolId")]
        public Guid StockId { get; set; }
        [JsonProperty("symbol")]
        public string StockSymbol { get; set; }
    }
}
