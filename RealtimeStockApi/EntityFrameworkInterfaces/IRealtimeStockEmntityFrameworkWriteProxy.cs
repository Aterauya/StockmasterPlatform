using RealtimeStockApi.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockApi.EntityFrameworkInterfaces
{
    public interface IRealtimeStockEmntityFrameworkWriteProxy
    {
        Task AddRealTimeStock(StockDataIngestedDTO stock);
    }
}
