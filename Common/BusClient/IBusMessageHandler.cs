using Microsoft.Azure.ServiceBus;

namespace Common.BusClient
{
    /// <summary>
    /// The bus message handler interface
    /// </summary>
    public interface IBusMessageHandler
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(Message message);
    }
}
