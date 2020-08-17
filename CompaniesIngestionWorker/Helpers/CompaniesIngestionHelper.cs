using CompaniesApi.Interfaces;
using Coravel.Invocable;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompaniesIngestionWorker.Helpers
{
    class CompaniesIngestionHelper : IInvocable
    {
        public async Task Invoke()
        {
            await GetCompanyStockSymbols();
        }

        private async Task GetCompanyStockSymbols()
        {
            var baseUrl = "https://finnhub.io/api/v1/stock/symbol?exchange=US&token=brtr40nrh5r9gcjm1mjg";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    Console.WriteLine(data);
                }
            }
        }
    }
}
