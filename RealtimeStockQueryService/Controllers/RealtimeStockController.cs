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
    /// <summary>
    /// The realtime stock controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RealtimeStockController : Controller
    {
        /// <summary>
        /// The read proxy
        /// </summary>
        private readonly IRealtimeStockReadProxy _readProxy;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<RealtimeStockController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealtimeStockController"/> class.
        /// </summary>
        /// <param name="readProxy">The read proxy.</param>
        /// <param name="logger">The logger.</param>
        public RealtimeStockController(IRealtimeStockReadProxy readProxy, ILogger<RealtimeStockController> logger)
        {
            _readProxy = readProxy;
            _logger = logger;
        }

        /// <summary>
        /// Gets the realtime stock.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>A list of realtime stock data transfer object</returns>
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
