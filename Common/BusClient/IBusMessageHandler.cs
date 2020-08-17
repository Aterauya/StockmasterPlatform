using Microsoft.Azure.ServiceBus;

namespace Common.BusClient
{
    public interface IBusMessageHandler
    {
        void Handle(Message message);
    }
}
