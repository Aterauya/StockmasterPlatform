namespace HistoricalStockApi.Interfaces
{
    /// <summary>
    /// The historical stock url helper interface
    /// </summary>
    public interface IHistoricalStockUrlHelper
    {
        /// <summary>
        /// Gets the candle URL.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <param name="resolution">The resolution.</param>
        /// <param name="timeFrom">The time from.</param>
        /// <param name="timeTo">The time to.</param>
        /// <returns>The candle url</returns>
        string GetCandleUrl(string stockSymbol, int resolution, long timeFrom, long timeTo);
    }
}
