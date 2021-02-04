using Common.BusClient;
using RealtimeStockApi.DataTransferObjects;

namespace RealtimeStockApi.Events
{
    /// <summary>
    /// The realtime stock ingested message
    /// </summary>
    /// <seealso cref="Common.BusClient.BusMessage" />
    public class RealtimeStockIngestedMessage : BusMessage
    {
        /// <summary>
        /// Gets or sets the stock ingested.
        /// </summary>
        /// <value>
        /// The stock ingested.
        /// </value>
        public StockDataIngestedDTO StockIngested { get; set; }
    }
}
