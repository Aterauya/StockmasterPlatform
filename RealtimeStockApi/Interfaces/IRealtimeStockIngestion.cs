using Common.Interfaces;
using MassTransit;
using RealtimeStockApi.EntityFrameworkInterfaces;
using System.Threading.Tasks;

namespace RealtimeStockApi
{
    public interface IRealtimeStockIngestion
    {
        void StartIngestion();
    }
}
