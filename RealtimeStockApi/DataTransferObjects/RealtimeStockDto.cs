using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RealtimeStockApi.DataTransferObjects
{
    public class RealtimeStockDto
    {
        public decimal Price { get; set; }

        public string StockSymbol { get; set; }

        public DateTime DateTimeTraded { get; set; }

        public decimal Volume { get; set; }
    }
}
