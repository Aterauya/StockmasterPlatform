using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.BusClient;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Common.AzureServiceBusClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Common.BusClient.IBusMessageConsumer" />
    public class AzureServiceBusMessageConsumer : IBusMessageConsumer
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
        /// The message handler
        /// </summary>
        private readonly IBusMessageHandler _messageHandler;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusMessageConsumer"/> class.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        /// <param name="config">The configuration.</param>
        public AzureServiceBusMessageConsumer(IBusMessageHandler messageHandler, IConfiguration config)
        {
            _config = config;
            _queueName = _config["queueName"];
            _queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBusConnectionString"), _queueName);
            _messageHandler = messageHandler;
        }

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task ProcessMessage(Message message, CancellationToken cancellationToken)
        {
            _messageHandler.Handle(message);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        /// <summary>
        /// Registers the consumer.
        /// </summary>
        public void RegisterConsumer()
        {
            var messageHandlerOptions = new MessageHandlerOptions(HandleMessageException)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _queueClient.RegisterMessageHandler(ProcessMessage, messageHandlerOptions);
        }

        /// <summary>
        /// Handles the message exception.
        /// </summary>
        /// <param name="exceptionReceivedEventArgs">The <see cref="ExceptionReceivedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private Task HandleMessageException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception.ToString(), "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }
    }
}
