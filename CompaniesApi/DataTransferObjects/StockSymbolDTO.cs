using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.DataTransferObjects
{
    public class StockSymbolDTO
    {
        public Guid SymbolId { get; set; }
        public string Symbol { get; set; }
    }
}
