using Common.BusClient;
using Newtonsoft.Json;
using RealtimeStockApi;
using RealtimeStockApi.DataTransferObjects;
using RealtimeStockApi.Events;
using RealtimeStockApi.Interfaces;
using System;
using System.Collections.Generic;
using Common.Auth;
using Microsoft.Extensions.Configuration;
using RealtimeStockApi.EntityFrameworkInterfaces;
using WebSocketSharp;
using RestSharp;

namespace RealtimeStockIngestion.Helpers
{
    /// <summary>
    /// Realtime ingestion helper
    /// </summary>
    /// <seealso cref="RealtimeStockApi.IRealtimeStockIngestion" />
    public class RealtimeIngestionHelper : IRealtimeStockIngestion
    {
        private readonly IRealtimeStockUrlHelper _urlHelper;
        private readonly IRealtimeStockWriteProxy _writeProxy;
        private readonly IConfiguration _configuration;
        public RealtimeIngestionHelper(IRealtimeStockUrlHelper urlHelper, IRealtimeStockWriteProxy writeProxy, IConfiguration configuration)
        {
            _urlHelper = urlHelper;
            _writeProxy = writeProxy;
            _configuration = configuration;
        }

        public void StartIngestion()
        {
            using (var ws = new WebSocket(_urlHelper.GetFinnhubRealtimeStockUrl()))
            {
                ws.OnMessage += async (sender, e) =>
                {
                    var dataJson = JsonConvert.DeserializeObject<StockDataIngestedDTO>(e.Data);
                    if (dataJson.Type.Equals("trade"))
                    {
                        _writeProxy.AddRealTimeStock(dataJson);
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
                        Console.WriteLine("Listening for trades from " + stockRequest.Symbol);
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
            var token = GetAuthToken();

            var client = new RestClient($"{_configuration.GetSection("CompaniesServiceBaseUrl").Value}" +
                                        $"{_configuration.GetSection("GetAllSymbolsEndpoint").Value}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"{token.TokenType} {token.AccessToken}");
            IRestResponse response = client.Execute(request);

            var stockList = JsonConvert.DeserializeObject<List<StockSymbolsDTO>>(response.Content);

            var copyOfStockList = new List<StockSymbolsDTO>();

            var counter = 0;

            foreach (var stock in stockList)
            {
                if (counter % 5 == 0)
                {
                    copyOfStockList.Add(stock);
                }
                counter++;
            }

            return copyOfStockList;
        }

        private AuthToken GetAuthToken()
        {
            var tokenClient = new RestClient(_configuration.GetSection("AuthTokenUrl").Value);
            var tokenRequest = new RestRequest(Method.POST); 
            tokenRequest.AddHeader("content-type", "application/json");
            var param = new RequestAuthToken(_configuration.GetSection("ClientId").Value, 
                _configuration.GetSection("ClientSecret").Value, _configuration.GetSection("CompanyInformationAudience").Value,
                _configuration.GetSection("GrantType").Value);

            tokenRequest.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);
            IRestResponse tokenResponse = tokenClient.Execute(tokenRequest);

            return JsonConvert.DeserializeObject<AuthToken>(tokenResponse.Content);
        }
    }
}
