using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealtimeStockApi.DataTransferObjects;

namespace RealtimeStockApi.EntityFrameworkInterfaces
{
    /// <summary>
    /// Realtime stock ready proxy interface
    /// </summary>
    public interface IRealtimeStockReadProxy
    {
        /// <summary>
        /// Gets the realtime stocks.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>A list of realtime stocks</returns>
        Task<List<RealtimeStockDto>> GetRealtimeStocks(string stockSymbol);
    }
}
