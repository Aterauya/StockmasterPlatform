using Newtonsoft.Json;
using RealtimeStockApi;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockCommandService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocketSharp;


namespace RealtimeStockCommandService.Handlers
{
    public class StockIngestionHandler : IRealtimeStockIngestion
    {

        public void StartIngestion()
        {
            using (var ws = new WebSocket(UrlHelper.GetFinnhubUrl()))
            {
                ws.OnMessage += (sender, e) =>
                {
                    var dataJson = JsonConvert.DeserializeObject<StockDataIngestedDTO>(e.Data);
                    if(dataJson.Type.Equals("trade"))
                    {
                        Console.WriteLine("Stock symbol: " + dataJson.StockIngested.StockSymbol);
                    }
                };

                ws.OnError += (sender, e) =>
                {
                    Console.WriteLine(e.Message);
                };

                ws.OnOpen += (sender, e) =>
                {
                    var stocksToRequest = getStockSymbols();
                    foreach (var item in stocksToRequest)
                    {
                        var stockRequest = new StockRequestDTO
                        {
                            Type = "subscribe",
                            Symbol = item.StockSymbol
                        };
                        ws.Send(JsonConvert.SerializeObject(stockRequest));
                    }
                };

                ws.OnClose += (sender, e) =>
                {
                    Console.WriteLine("Reason: " + e.Reason + " Code: " + e.Code);
                };
            }
        }

        public List<StockSymbolsDTO> getStockSymbols()
        {
            var stockRequest = new StockSymbolsDTO
            {
                StockId = Guid.NewGuid(),
                StockSymbol = "BINANCE:BTCUSDT"
            };
            var stockList = new List<StockSymbolsDTO>();
            stockList.Add(stockRequest);
            return stockList;
        }
    }
}
