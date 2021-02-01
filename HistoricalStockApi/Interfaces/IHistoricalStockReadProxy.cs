using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;

namespace HistoricalStockApi.Interfaces
{
    public interface IHistoricalStockReadProxy
    {
        Task<List<HistoricalStockDto>> GetHistoricalStocksForCompany(string stockSymbol);
    }
}
