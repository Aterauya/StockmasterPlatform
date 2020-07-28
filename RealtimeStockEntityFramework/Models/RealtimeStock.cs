using System;
using System.Collections.Generic;

namespace RealtimeStockEntityFramework.Models
{
    public partial class RealtimeStock
    {
        public Guid RealtimeStockId { get; set; }
        public decimal Price { get; set; }
        public string StockSymbol { get; set; }
        public DateTime DateTimeTraded { get; set; }
        public decimal Volume { get; set; }
    }
}
