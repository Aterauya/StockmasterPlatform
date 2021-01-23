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
    public class RealtimeStockMessageHandler : IBusMessageHandler
    {
        private readonly IRealtimeStockWriteProxy _realtimeStockWriteProxy;
        public RealtimeStockMessageHandler(IRealtimeStockWriteProxy realtimeStockWriteProxy)
        {
            _realtimeStockWriteProxy = realtimeStockWriteProxy;
        }
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
