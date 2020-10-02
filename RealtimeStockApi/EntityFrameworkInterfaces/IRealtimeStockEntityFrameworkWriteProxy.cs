using RealtimeStockApi.DataTransferObjects;
using System.Threading.Tasks;

namespace RealtimeStockApi.EntityFrameworkInterfaces
{
    public interface IRealtimeStockEntityFrameworkWriteProxy
    {
        Task AddRealTimeStock(StockDataIngestedDTO stock);
    }
}
