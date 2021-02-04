using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.DataTransferObjects
{
    /// <summary>
    /// The Stock Symbol Data Transfer Object
    /// </summary>
    public class StockSymbolDTO
    {
        /// <summary>
        /// Gets or sets the symbol identifier.
        /// </summary>
        /// <value>
        /// The symbol identifier.
        /// </value>
        public Guid SymbolId { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>
        /// The symbol.
        /// </value>
        public string Symbol { get; set; }
    }
}
