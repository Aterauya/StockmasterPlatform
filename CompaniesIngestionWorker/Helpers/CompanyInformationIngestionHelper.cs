using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CompaniesIngestionWorker.Helpers
{
    /// <summary>
    /// The company information ingestion helper
    /// </summary>
    /// <seealso cref="Coravel.Invocable.IInvocable" />
    public class CompanyInformationIngestionHelper : IInvocable
    {
        /// <summary>
        /// The read proxy
        /// </summary>
        private readonly ICompanyReadProxy _readProxy;

        /// <summary>
        /// The write proxy
        /// </summary>
        private readonly ICompanyWriteProxy _writeProxy;

        /// <summary>
        /// The URL helper
        /// </summary>
        private readonly ICompanyUrlHelper _urlHelper;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CompanyInformationIngestionHelper> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyInformationIngestionHelper"/> class.
        /// </summary>
        /// <param name="readProxy">The read proxy.</param>
        /// <param name="writeProxy">The write proxy.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="logger">The logger.</param>
        public CompanyInformationIngestionHelper(ICompanyReadProxy readProxy, ICompanyWriteProxy writeProxy, ICompanyUrlHelper urlHelper, 
            ILogger<CompanyInformationIngestionHelper> logger)
        {
            _readProxy = readProxy;
            _writeProxy = writeProxy;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public async Task Invoke()
        {
            var symbols = await GetCompanySymbols();

            foreach (var stockSymbol in symbols)
            {
                var information = await GetCompanyInformation(stockSymbol);

                if (information.Name != null)
                {
                    await _writeProxy.AddCompanyInformation(information);
                    _logger.LogInformation($"Adding information for {information.Name}");
                }
            }
        }

        /// <summary>
        /// Gets the company information.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol.</param>
        /// <returns>Company information</returns>
        private async Task<CompanyInformationDto> GetCompanyInformation(StockSymbolDTO stockSymbol)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(_urlHelper.GetFinnhubCompanyProfileUrl(stockSymbol.Symbol)))
            using (HttpContent content = res.Content)
            {
                var companyInformation = new CompanyInformationDto();

                if (res.IsSuccessStatusCode)
                {
                    var data = await content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(data))
                    {
                        try
                        {
                            companyInformation = JsonConvert.DeserializeObject<CompanyInformationDto>(data);
                            companyInformation.SymbolId = stockSymbol.SymbolId;
                        }
                        catch (JsonSerializationException e)
                        {
                            _logger.LogError(e.Message);
                        }
                    }
                }
                else
                {
                    _logger.LogError(res.ReasonPhrase);
                    await Task.Delay(60000);
                }
                return companyInformation;
            }
        }

        /// <summary>
        /// Gets the company symbols.
        /// </summary>
        /// <returns>A list of stock symbols</returns>
        private async Task<List<StockSymbolDTO>> GetCompanySymbols()
        {
            return await _readProxy.GetCompanySymbols();
        }
    }
}
