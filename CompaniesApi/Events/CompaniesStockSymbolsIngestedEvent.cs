using Common.BusClient;
using CompaniesApi.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.Events
{
    public class CompaniesStockSymbolsIngestedEvent : BusMessage
    {
        public List<StockSymbolDTO> StockSymbols { get; set; }
    }
}
