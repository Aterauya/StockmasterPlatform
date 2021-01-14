using System;
using System.Collections.Generic;

namespace CompaniesEntityFramework.Models
{
    public partial class CompanyInformation
    {
        public Guid CompanyId { get; set; }
        public Guid SymbolId { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public DateTime Ipo { get; set; }
        public decimal MarketCapitalization { get; set; }
        public float OutstandingShares { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }
        public string CountryName { get; set; }
        public string CurrencyName { get; set; }
        public string IndustryName { get; set; }

        public virtual CompanySymbol Symbol { get; set; }
    }
}
