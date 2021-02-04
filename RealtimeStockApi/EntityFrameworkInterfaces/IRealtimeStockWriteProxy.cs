using RealtimeStockApi.DataTransferObjects;
using System.Threading.Tasks;

namespace RealtimeStockApi.EntityFrameworkInterfaces
{
    /// <summary>
    /// Realtimestock write proxy interface
    /// </summary>
    public interface IRealtimeStockWriteProxy
    {
        /// <summary>
        /// Adds the real time stock.
        /// </summary>
        /// <param name="stock">The stock.</param>
        void AddRealTimeStock(StockDataIngestedDTO stock);
    }
}
