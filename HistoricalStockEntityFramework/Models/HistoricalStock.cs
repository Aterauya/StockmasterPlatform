using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalStockEntityFramework.Models
{
    public class HistoricalStock
    {
        public Guid HistoricalStockId { get; set; }
        public string StockSymbol { get; set; }
        public double OpeningPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double ClosePrice { get; set; }
        public decimal Volume { get; set; }
        public DateTime ClosingDateTime { get; set; }
        public string FilterHash { get; set; }
    }
}
