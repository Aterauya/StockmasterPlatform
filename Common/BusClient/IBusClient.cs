using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusClient
{
    public interface IBusClient
    {
        public Task SendMessage(BusMessage busMessage);
    }
}
