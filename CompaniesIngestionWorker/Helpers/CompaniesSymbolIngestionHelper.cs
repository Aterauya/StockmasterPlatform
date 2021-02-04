using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using Coravel.Invocable;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompaniesIngestionWorker.Helpers
{
    /// <summary>
    /// The companies symbol ingestion helper
    /// </summary>
    /// <seealso cref="Coravel.Invocable.IInvocable" />
    public class CompaniesSymbolIngestionHelper : IInvocable
    {
        /// <summary>
        /// The URL helper
        /// </summary>
        private readonly ICompanyUrlHelper _urlHelper;

        /// <summary>
        /// The write proxy
        /// </summary>
        private readonly ICompanyWriteProxy _writeProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesSymbolIngestionHelper"/> class.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="writeProxy">The write proxy.</param>
        public CompaniesSymbolIngestionHelper(ICompanyUrlHelper urlHelper,ICompanyWriteProxy writeProxy)
        {
            _urlHelper = urlHelper;
            _writeProxy = writeProxy;
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public async Task Invoke()
        {
            var symbols = await GetCompanyStockSymbols();

            await _writeProxy.AddCompanySymbols(symbols);
        }

        /// <summary>
        /// Gets the company stock symbols.
        /// </summary>
        /// <returns>A list of stock symbols</returns>
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
