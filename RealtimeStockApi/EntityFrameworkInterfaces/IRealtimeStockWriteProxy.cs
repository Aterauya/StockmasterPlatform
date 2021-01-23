using RealtimeStockApi.DataTransferObjects;
using System.Threading.Tasks;

namespace RealtimeStockApi.EntityFrameworkInterfaces
{
    public interface IRealtimeStockWriteProxy
    {
        void AddRealTimeStock(StockDataIngestedDTO stock);
    }
}
