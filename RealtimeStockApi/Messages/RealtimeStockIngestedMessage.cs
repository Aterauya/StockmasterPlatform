using Common.BusClient;
using RealtimeStockApi.DataTransferObjects;

namespace RealtimeStockApi.Events
{
    public class RealtimeStockIngestedMessage : BusMessage
    {
        public StockDataIngestedDTO StockIngested { get; set; }
    }
}
