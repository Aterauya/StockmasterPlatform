using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RealtimeStockIngestion.Helpers
{
    public class UrlHelper : IUrlHelper
    {

        public string GetFinnhubUrl()
        {
            var url = ConfigurationManager.AppSettings["finnHubUrl"];
            var token = ConfigurationManager.AppSettings["finnHubToken"];

            return url + "?token=" + token;
        }
    }
}
