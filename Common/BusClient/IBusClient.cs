using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusClient
{
    /// <summary>
    /// The bus client interface
    /// </summary>
    public interface IBusClient
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="busMessage">The bus message.</param>
        /// <returns></returns>
        public Task SendMessage(BusMessage busMessage);
    }
}
