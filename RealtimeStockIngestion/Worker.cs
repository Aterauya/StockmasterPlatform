using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RealtimeStockIngestion
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRealtimeStockIngestion _realtimeStockIngestion;

        public Worker(ILogger<Worker> logger, IRealtimeStockIngestion realtimeStockIngestion)
        {
            _logger = logger;
            _realtimeStockIngestion = realtimeStockIngestion;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _realtimeStockIngestion.StartIngestion();
            }
        }
    }
}
