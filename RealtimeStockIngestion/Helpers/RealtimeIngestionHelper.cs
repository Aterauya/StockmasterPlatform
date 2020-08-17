using Common.DataTransferObjects;
using Newtonsoft.Json;
using RealtimeStockApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using WebSocketSharp;
using System.Threading.Tasks;
using Common.BusClient;
using Common.Events;
using Microsoft.Extensions.Configuration;
using Common.Interfaces;

namespace RealtimeStockIngestion.Helpers
{
    public class RealtimeIngestionHelper : IRealtimeStockIngestion
    {
        private readonly IBusClient _busClient;
        private readonly IUrlHelper _urlHelper;
        public RealtimeIngestionHelper(IBusClient busClient, IUrlHelper urlHelper, IConfiguration configuration)
        {
            _busClient = busClient;
            _urlHelper = urlHelper;
        }

        public void StartIngestion()
        {
            using (var ws = new WebSocket(_urlHelper.GetFinnhubUrl()))
            {
                ws.OnMessage += async (sender, e) =>
                {
                    var dataJson = JsonConvert.DeserializeObject<StockDataIngestedDTO>(e.Data);
                    if (dataJson.Type.Equals("trade"))
                    {
                        await _busClient.SendMessage(new RealtimeStockIngestedEvent
                        { 
                            StockIngested = dataJson,
                        });
                    }
                };

                ws.OnError += (sender, e) =>
                {
                    Console.WriteLine(e.Message);
                };

                ws.OnOpen += (sender, e) =>
                {
                    var stocksToRequest = GetStockSymbols();
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

                ws.Connect();
                Console.ReadKey();
            }
        }

        public List<StockSymbolsDTO> GetStockSymbols()
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
