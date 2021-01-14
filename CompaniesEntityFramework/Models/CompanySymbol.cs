using System;
using System.Collections.Generic;

namespace CompaniesEntityFramework.Models
{
    public partial class CompanySymbol
    {
        public CompanySymbol()
        {
            CompanyInformation = new HashSet<CompanyInformation>();
        }

        public Guid SymbolId { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<CompanyInformation> CompanyInformation { get; set; }
    }
}
