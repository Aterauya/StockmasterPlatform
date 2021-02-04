using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RealtimeStockApi.DataTransferObjects
{
    /// <summary>
    /// Realtime stock data transfer object
    /// </summary>
    public class RealtimeStockDto
    {
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the date time traded.
        /// </summary>
        /// <value>
        /// The date time traded.
        /// </value>
        public DateTime DateTimeTraded { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public decimal Volume { get; set; }
    }
}
