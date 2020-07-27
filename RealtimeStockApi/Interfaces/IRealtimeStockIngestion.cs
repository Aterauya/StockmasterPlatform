using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockApi
{
    public interface IRealtimeStockIngestion
    {
        /// <summary>
        /// Starts the ingestion of realtime stock
        /// </summary>
        void StartIngestion();
    }
}
