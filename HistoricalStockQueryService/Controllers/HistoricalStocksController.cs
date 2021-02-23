using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HistoricalStockQueryService.Controllers
{
    /// <summary>
    /// The historical stock controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalStocksController : Controller
    {
        private readonly IHistoricalStockReadProxy _readProxy;
        private readonly ILogger<HistoricalStocksController> _logger;

        public HistoricalStocksController(IHistoricalStockReadProxy readProxy, ILogger<HistoricalStocksController> logger)
        {
            _readProxy = readProxy;
            _logger = logger;
        }

        /// <summary>
        /// Gets the historical stocks.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>A list of historical stocks</returns>
        [Route("GetHistoricalStocks/{stockSymbol}")]
        [HttpGet]
        [ProducesResponseType(typeof(HistoricalStockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<List<HistoricalStockDto>>> GetHistoricalStocks(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                _logger.LogError("Stock symbol is null or empty");
                return NotFound();
            }

            var historicalStocks = await _readProxy.GetHistoricalStocksForCompany(stockSymbol);

            if (!historicalStocks.Any())
            {
                _logger.LogError("No stocks for given stock symbol");
                return NotFound();
            }

            return historicalStocks;
        }


    }
}
