using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompaniesApi.DataTransferObjects;
using CompaniesApi.Interfaces;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;

namespace CompaniesIngestionWorker.Helpers
{
    public class CompanyInformationIngestionHelper : IInvocable
    {
        private readonly ICompanyReadProxy _readProxy;
        private readonly ICompanyWriteProxy _writeProxy;
        private readonly ICompanyUrlHelper _urlHelper;
        public CompanyInformationIngestionHelper(ICompanyReadProxy readProxy, ICompanyWriteProxy writeProxy, ICompanyUrlHelper urlHelper)
        {
            _readProxy = readProxy;
            _writeProxy = writeProxy;
            _urlHelper = urlHelper;
        }
        public async Task Invoke()
        {
            var symbols = await GetCompanySymbols();

            var companyInformation = new List<CompanyInformationDto>();


            foreach (var stockSymbol in symbols)
            {
                var information = await GetCompanyInformation(stockSymbol);

                if (information.Name != null)
                {
                    companyInformation.Add(information);
                }
            }

            await _writeProxy.AddCompanyInformation(companyInformation);
        }

        private async Task<CompanyInformationDto> GetCompanyInformation(StockSymbolDTO stockSymbol)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(_urlHelper.GetFinnhubCompanyProfileUrl(stockSymbol.Symbol)))
            using (HttpContent content = res.Content)
            { 
                var companyInformation = new CompanyInformationDto();
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
                        Console.WriteLine(e);
                    }
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
