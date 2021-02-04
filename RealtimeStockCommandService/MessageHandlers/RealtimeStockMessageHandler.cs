using Common.BusClient;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;
using RealtimeStockApi.Events;
using System;
using System.Text;

namespace RealtimeStockCommandService.MessageHandlers
{
    /// <summary>
    /// The realtime stock message handler
    /// </summary>
    /// <seealso cref="Common.BusClient.IBusMessageHandler" />
    public class RealtimeStockMessageHandler : IBusMessageHandler
    {
        /// <summary>
        /// The realtime stock write proxy
        /// </summary>
        private readonly IRealtimeStockWriteProxy _realtimeStockWriteProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealtimeStockMessageHandler"/> class.
        /// </summary>
        /// <param name="realtimeStockWriteProxy">The realtime stock write proxy.</param>
        public RealtimeStockMessageHandler(IRealtimeStockWriteProxy realtimeStockWriteProxy)
        {
            _realtimeStockWriteProxy = realtimeStockWriteProxy;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(Message message)
        {
            var data = JsonConvert.DeserializeObject<RealtimeStockIngestedMessage>(Encoding.UTF8.GetString(message.Body));
            var payload = new StockDataIngestedDTO()
            {
                StockIngested = data.StockIngested.StockIngested,
                Type = data.StockIngested.Type
            };
            _realtimeStockWriteProxy.AddRealTimeStock(payload);
        }
    }
}
