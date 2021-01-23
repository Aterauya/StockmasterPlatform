using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using Coravel.Invocable;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompaniesIngestionWorker.Helpers
{
    public class CompaniesSymbolIngestionHelper : IInvocable
    {
        private readonly ICompanyUrlHelper _urlHelper;
        private readonly ICompanyWriteProxy _writeProxy;
        public CompaniesSymbolIngestionHelper(ICompanyUrlHelper urlHelper,ICompanyWriteProxy writeProxy)
        {
            _urlHelper = urlHelper;
            _writeProxy = writeProxy;
        }

        public async Task Invoke()
        {
            var symbols = await GetCompanyStockSymbols();

            await _writeProxy.AddCompanySymbols(symbols);
        }

        private async Task<List<StockSymbolDTO>> GetCompanyStockSymbols()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(_urlHelper.GetFinnhubCompaniesSymbolUrl()))
            using (HttpContent content = res.Content)
            {
                var data = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<StockSymbolDTO>>(data);
            }
        }
    }
}
