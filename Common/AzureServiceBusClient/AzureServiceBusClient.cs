using Common.BusClient;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Common.AzureServiceBusClient
{
    public class AzureServiceBusClient : IBusClient
    {
        private readonly QueueClient _queueClient;
        private readonly string _queueName;
        public AzureServiceBusClient(IConfiguration configuration)
        {
            _queueName = configuration["queueName"];
            _queueClient = new QueueClient(configuration.GetConnectionString("AzureServiceBusConnectionString"), _queueName);
        }

        public async Task SendMessage(BusMessage busMessage)
        {
            string data = JsonConvert.SerializeObject(busMessage);
            var message = new Message
            {
                Body = Encoding.UTF8.GetBytes(data)
            };
            await _queueClient.SendAsync(message);
        }
    }
}
