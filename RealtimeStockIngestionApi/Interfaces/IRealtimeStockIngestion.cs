using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockIngestionApi.Interfaces
{
    interface IRealtimeStockIngestion
    {
        Task StartIngestion();
    }
}
