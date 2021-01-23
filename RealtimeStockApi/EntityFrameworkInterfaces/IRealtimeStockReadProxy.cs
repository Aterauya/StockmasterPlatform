using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealtimeStockApi.DataTransferObjects;

namespace RealtimeStockApi.EntityFrameworkInterfaces
{
    public interface IRealtimeStockReadProxy
    {
        Task<List<RealtimeStockDto>> GetRealtimeStocks(string stockSymbol);
    }
}
