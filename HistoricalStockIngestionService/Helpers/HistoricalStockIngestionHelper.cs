using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common.Auth;
using Coravel.Invocable;
using HistoricalStockApi;
using HistoricalStockApi.DataTransferObjects;
using HistoricalStockApi.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using RestSharp;

namespace HistoricalStockIngestionService.Helpers
{
    public class HistoricalStockIngestionHelper : IInvocable
    {
        private readonly IHistoricalStockUrlHelper _urlHelper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HistoricalStockIngestionHelper> _logger;
        private readonly IHistoricalStockWriteProxy _writeProxy;

        public HistoricalStockIngestionHelper(IHistoricalStockUrlHelper urlHelper, IConfiguration configuration, 
            ILogger<HistoricalStockIngestionHelper> logger, IHistoricalStockWriteProxy writeProxy)
        {
            _urlHelper = urlHelper;
            _configuration = configuration;
            _logger = logger;
            _writeProxy = writeProxy;
        }

        public async Task Invoke()
        {
            var comapnySymbols = GetStockSymbols();

            var dateTo = DateTimeOffset.Now.ToUnixTimeSeconds();
            var dateFrom = DateTimeOffset.Now.AddYears(-1).ToUnixTimeSeconds();

            foreach (var companySymbol in comapnySymbols)
            {
                var stocks = await GetHistoricalStocks(companySymbol.StockSymbol, dateFrom, dateTo);

                if (stocks.Any())
                {
                    _logger.LogInformation($"Adding stocks from {companySymbol.StockSymbol}");
                    await _writeProxy.AddHistoricalStock(stocks);
                }
            }
        }

        public async Task<List<HistoricalStockDto>> GetHistoricalStocks(string symbol, long dateTo, long dateFrom)
        {
            var historicalStocks = new List<HistoricalStockDto>();

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res =
                await client.GetAsync(_urlHelper.GetCandleUrl(symbol, 1, dateTo, dateFrom)))
            using (HttpContent content = res.Content)
            { 
                if (res.IsSuccessStatusCode)
                {
                    var data = await content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(data) && !data.Contains("no_data"))
                    {
                        var ingestedData = JsonConvert.DeserializeObject<HistoricalStockDataIngestedDto>(data);
                        for (int i = 0; i < ingestedData.ClosePrice.Count; i++)
                        {
                            historicalStocks.Add(new HistoricalStockDto
                            {
                                ClosePrice = ingestedData.ClosePrice[i],
                                HighPrice = ingestedData.HighPrice[i],
                                LowPrice = ingestedData.LowPrice[i],
                                OpeningPrice = ingestedData.OpeningPrice[i],
                                ClosingDateTime = DateTimeOffset.FromUnixTimeSeconds(ingestedData.ClosingDateTime[i]).UtcDateTime,
                                Volume = ingestedData.Volume[i],
                                StockSymbol = symbol
                            });
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"Ingestion failed for {symbol}");
                    }

                }
                else
                {
                    _logger.LogError(res.ReasonPhrase);
                    await Task.Delay(60000);
                }
            }
            return historicalStocks;

        }

        public List<StockSymbolsDto> GetStockSymbols()
        {
            var token = GetAuthToken();

            var client = new RestClient($"{_configuration.GetSection("CompaniesServiceBaseUrl").Value}" +
                                        $"{_configuration.GetSection("GetAllSymbolsEndpoint").Value}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"{token.TokenType} {token.AccessToken}");
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<StockSymbolsDto>>(response.Content);
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
