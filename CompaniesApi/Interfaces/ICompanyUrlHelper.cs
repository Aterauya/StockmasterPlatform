using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.Interfaces
{
    public interface ICompanyUrlHelper
    {
        string GetFinnhubCompaniesSymbolUrl();

        string GetFinnhubCompanyProfileUrl(string ticker);
    }
}
