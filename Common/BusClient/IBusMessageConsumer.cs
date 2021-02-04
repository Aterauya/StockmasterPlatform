using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BusClient
{
    /// <summary>
    /// The bus message consumer interface
    /// </summary>
    public interface IBusMessageConsumer
    {
        /// <summary>
        /// Registers the consumer.
        /// </summary>
        void RegisterConsumer();

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task ProcessMessage(Message message, CancellationToken cancellationToken);
    }
}
