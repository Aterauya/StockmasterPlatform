using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalStockApi.DataTransferObjects
{
    /// <summary>
    /// The historical stock data transfer object
    /// </summary>
    public class HistoricalStockDto
    {
        /// <summary>
        /// Gets or sets the historical stock identifier.
        /// </summary>
        /// <value>
        /// The historical stock identifier.
        /// </value>
        public Guid HistoricalStockId { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the opening price.
        /// </summary>
        /// <value>
        /// The opening price.
        /// </value>
        public double OpeningPrice { get; set; }

        /// <summary>
        /// Gets or sets the high price.
        /// </summary>
        /// <value>
        /// The high price.
        /// </value>
        public double HighPrice { get; set; }

        /// <summary>
        /// Gets or sets the low price.
        /// </summary>
        /// <value>
        /// The low price.
        /// </value>
        public double LowPrice { get; set; }

        /// <summary>
        /// Gets or sets the close price.
        /// </summary>
        /// <value>
        /// The close price.
        /// </value>
        public double ClosePrice { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public decimal Volume { get; set; }

        /// <summary>
        /// Gets or sets the closing date time.
        /// </summary>
        /// <value>
        /// The closing date time.
        /// </value>
        public DateTime ClosingDateTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the filter hash.
        /// </summary>
        /// <value>
        /// The filter hash.
        /// </value>
        public string FilterHash { get; set; }
    }
}
