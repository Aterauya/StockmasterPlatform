using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.DataTransferObjects
{
    public class StockSymbolDTO
    {
        public string Description { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
    }
}
