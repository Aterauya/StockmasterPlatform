using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CompaniesApi.DataTransferObjects
{
    public class CompanyInformationDto
    {
        public Guid CompanyId { get; set; }
        public Guid SymbolId { get; set; }
        public string CompanySymbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public DateTime? Ipo { get; set; }
        public decimal MarketCapitalization { get; set; }
        [JsonProperty("shareOutstanding")]
        public float OutstandingShares { get; set; }
        [JsonProperty("weburl")]
        public string Url { get; set; }
        public string Logo { get; set; }
        [JsonProperty("country")]
        public string CountryName { get; set; }
        [JsonProperty("currency")]
        public string CurrencyName { get; set; }
        [JsonProperty("finnhubIndustry")]
        public string IndustryName { get; set; }
    }
}
