using Common.BusClient;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Events;
using CompaniesApi.Interfaces;
using Coravel.Invocable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompaniesIngestionWorker.Helpers
{
    public class CompaniesIngestionHelper : IInvocable
    {
        private readonly ICompanyUrlHelper _urlHelper;
        private readonly IBusClient _busClient;
        public CompaniesIngestionHelper(ICompanyUrlHelper urlHelper, IBusClient busClient)
        {
            _urlHelper = urlHelper;
            _busClient = busClient;
        }

        public async Task Invoke()
        {
            var symbols = await GetCompanyStockSymbols();
            await _busClient.SendMessage(new CompaniesStockSymbolsIngestedEvent
            {
                StockSymbols = symbols
            });
        }

        private async Task<List<StockSymbolDTO>> GetCompanyStockSymbols()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(_urlHelper.GetFinnhubCompaniesUrl()))
            using (HttpContent content = res.Content)
            {
                var data = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<StockSymbolDTO>>(data);
            }
        }
    }
}
