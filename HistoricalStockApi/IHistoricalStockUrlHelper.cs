using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalStockApi
{
    public interface IHistoricalStockUrlHelper
    {
        string GetCandleUrl(string stockSymbol, int resolution, long timeFrom, long timeTo);
    }
}
