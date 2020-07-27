using System;
using System.Collections.Generic;
using System.Text;

namespace RealtimeStockApi.DataTransferObjects
{
    public class StockSymbolsDTO
    {
        public Guid StockId { get; set; }
        public string StockSymbol { get; set; }
    }
}
