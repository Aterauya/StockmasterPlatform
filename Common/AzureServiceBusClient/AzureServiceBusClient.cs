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
    /// <summary>
    /// The azure service bus client
    /// </summary>
    /// <seealso cref="Common.BusClient.IBusClient" />
    public class AzureServiceBusClient : IBusClient
    {
        /// <summary>
        /// The queue client
        /// </summary>
        private readonly QueueClient _queueClient;

        /// <summary>
        /// The queue name
        /// </summary>
        private readonly string _queueName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AzureServiceBusClient(IConfiguration configuration)
        {
            _queueName = configuration["queueName"];
            _queueClient = new QueueClient(configuration.GetConnectionString("AzureServiceBusConnectionString"), _queueName);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="busMessage">The bus message.</param>
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
