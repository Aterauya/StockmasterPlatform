using System;
using System.Collections.Generic;
using System.Text;

namespace RealtimeStockApi.Interfaces
{
    /// <summary>
    /// The realtime stock url helper interface
    /// </summary>
    public interface IRealtimeStockUrlHelper
    {
        /// <summary>
        /// Gets the finnhub realtime stock URL.
        /// </summary>
        /// <returns>The realtime stock url</returns>
        string GetFinnhubRealtimeStockUrl();
    }
}
