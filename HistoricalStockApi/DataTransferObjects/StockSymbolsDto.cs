using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HistoricalStockApi.DataTransferObjects
{
    public class StockSymbolsDto
    {
        [JsonProperty("symbolId")]
        public Guid StockId { get; set; }
        [JsonProperty("symbol")]
        public string StockSymbol { get; set; }
    }
}
