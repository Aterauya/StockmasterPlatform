using Microsoft.Extensions.Configuration;
using RealtimeStockApi.Interfaces;
using System.Configuration;

namespace RealtimeStockIngestion.Helpers
{
    public class RealtimeStockUrlHelper : IRealtimeStockUrlHelper
    {
        private readonly IConfiguration _configuration;
        public RealtimeStockUrlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetFinnhubRealtimeStockUrl()
        {
            return string.Format(_configuration["finnHubRealtimeStockUrl"], _configuration["finnHubToken"]);
        }
    }
}
