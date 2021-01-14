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
    public class AzureServiceBusMessageConsumer : IBusMessageConsumer
    {
        private readonly QueueClient _queueClient;
        private readonly string _queueName;
        private readonly IBusMessageHandler _messageHandler;
        private readonly IConfiguration _config;
        public AzureServiceBusMessageConsumer(IBusMessageHandler messageHandler, IConfiguration config)
        {
            _config = config;
            _queueName = _config["queueName"];
            _queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBusConnectionString"), _queueName);
            _messageHandler = messageHandler;
        }

        public async Task ProcessMessage(Message message, CancellationToken cancellationToken)
        {
            _messageHandler.Handle(message);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        public void RegisterConsumer()
        {
            var messageHandlerOptions = new MessageHandlerOptions(HandleMessageException)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _queueClient.RegisterMessageHandler(ProcessMessage, messageHandlerOptions);
        }

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
