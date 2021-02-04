using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;

namespace HistoricalStockApi.Interfaces
{
    /// <summary>
    /// The historical write proxy interface
    /// </summary>
    public interface IHistoricalStockWriteProxy
    {
        /// <summary>
        /// Adds the historical stock.
        /// </summary>
        /// <param name="historicalStocks">The historical stocks.</param>
        /// <returns></returns>
        Task AddHistoricalStock(List<HistoricalStockDto> historicalStocks);
    }
}
