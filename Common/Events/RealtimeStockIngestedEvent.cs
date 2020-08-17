using Common.BusClient;
using Common.DataTransferObjects;

namespace Common.Events
{
    public class RealtimeStockIngestedEvent : BusMessage
    {
        public StockDataIngestedDTO StockIngested { get; set; }
    }
}
