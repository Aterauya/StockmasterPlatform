using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;

namespace HistoricalStockApi.Interfaces
{
    /// <summary>
    /// The historical stock ready proxy interface
    /// </summary>
    public interface IHistoricalStockReadProxy
    {
        /// <summary>
        /// Gets the historical stocks for company.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>A list of historical stocks</returns>
        Task<List<HistoricalStockDto>> GetHistoricalStocksForCompany(string stockSymbol);
    }
}
