using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.EntityFrameworkInterfaces;

namespace RealtimeStockQueryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RealtimeStockController : Controller
    {
        private readonly IRealtimeStockReadProxy _readProxy;
        private readonly ILogger<RealtimeStockController> _logger;

        public RealtimeStockController(IRealtimeStockReadProxy readProxy, ILogger<RealtimeStockController> logger)
        {
            _readProxy = readProxy;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetRealtimeStock/{stockSymbol}")]
        [ProducesResponseType(typeof(List<RealtimeStockDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<List<RealtimeStockDto>>> GetRealtimeStock(string stockSymbol)
        {
            if (string.IsNullOrWhiteSpace(stockSymbol))
            {
                _logger.LogError("Parameter passed in is empty or null");
                return NotFound();
            }

            var realtimeStocks = await _readProxy.GetRealtimeStocks(stockSymbol);

            if (!realtimeStocks.Any())
            {
                _logger.LogError("There is no realtime stock for that stock symbol");
                return NotFound();
            }

            return realtimeStocks;
        }
    }
}
