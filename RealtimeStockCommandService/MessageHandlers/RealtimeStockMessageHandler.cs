using Common.BusClient;
using Common.DataTransferObjects;
using Common.Events;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using RealtimeStockApi.EntityFrameworkInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockCommandService.MessageHandlers
{
    public class RealtimeStockMessageHandler : IBusMessageHandler
    {
        private readonly IRealtimeStockEntityFrameworkWriteProxy _realtimeStockWriteProxy;
        public RealtimeStockMessageHandler(IRealtimeStockEntityFrameworkWriteProxy realtimeStockWriteProxy)
        {
            _realtimeStockWriteProxy = realtimeStockWriteProxy;
        }
        public void Handle(Message message)
        {
            var data = JsonConvert.DeserializeObject<RealtimeStockIngestedEvent>(Encoding.UTF8.GetString(message.Body));
            var payload = new StockDataIngestedDTO()
            {
                Id = Guid.NewGuid(),
                StockIngested = data.StockIngested.StockIngested,
                Type = data.StockIngested.Type
            };
            _realtimeStockWriteProxy.AddRealTimeStock(payload);
        }
    }
}
