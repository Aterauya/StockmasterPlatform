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
    public class CompanyInformationIngestionHelper : IInvocable
    {
        private readonly ICompanyReadProxy _readProxy;
        private readonly ICompanyWriteProxy _writeProxy;
        private readonly ICompanyUrlHelper _urlHelper;
        private readonly ILogger<CompanyInformationIngestionHelper> _logger;
        public CompanyInformationIngestionHelper(ICompanyReadProxy readProxy, ICompanyWriteProxy writeProxy, ICompanyUrlHelper urlHelper, 
            ILogger<CompanyInformationIngestionHelper> logger)
        {
            _readProxy = readProxy;
            _writeProxy = writeProxy;
            _urlHelper = urlHelper;
            _logger = logger;
        }
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

        private async Task<List<StockSymbolDTO>> GetCompanySymbols()
        {
            return await _readProxy.GetCompanySymbols();
        }
    }
}
