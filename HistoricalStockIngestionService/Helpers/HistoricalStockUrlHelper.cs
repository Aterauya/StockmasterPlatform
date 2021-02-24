using System;
using System.Collections.Generic;
using System.Text;
using HistoricalStockApi;
using HistoricalStockApi.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HistoricalStockIngestionService.Helpers
{
    public class HistoricalStockUrlHelper : IHistoricalStockUrlHelper
    {
        private readonly IConfiguration _configuration;

        public HistoricalStockUrlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCandleUrl(string stockSymbol, char resolution, long timeFrom, long timeTo)
        { 
            var url = string.Format(_configuration.GetSection("finnHubCandleUrl").Value, new object[]{stockSymbol, resolution, timeFrom, 
                timeTo, _configuration.GetSection("finnHubToken").Value});
            return url;
        }
    }
}
