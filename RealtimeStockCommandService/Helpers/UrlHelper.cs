using System.Configuration;

namespace RealtimeStockCommandService.Helpers
{
    public class UrlHelper
    {
        public static string GetFinnhubUrl()
        {
            var url = ConfigurationManager.AppSettings["finnHubUrl"];
            var token = ConfigurationManager.AppSettings["finnHubToken"];

            return url + "?token=" + token;
        }
    }
}
