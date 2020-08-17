using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BusClient
{
    public interface IBusMessageConsumer
    {
        void RegisterConsumer();

        Task ProcessMessage(Message message, CancellationToken cancellationToken);
    }
}
