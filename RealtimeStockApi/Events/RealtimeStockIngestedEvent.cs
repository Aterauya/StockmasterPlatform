using Common.BusClient;
using RealtimeStockApi.DataTransferObjects;

namespace RealtimeStockApi.Events
{
    public class RealtimeStockIngestedEvent : BusMessage
    {
        public StockDataIngestedDTO StockIngested { get; set; }
    }
}
