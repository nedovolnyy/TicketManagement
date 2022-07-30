using System.Web.Mvc;
using log4net;

namespace ThirdPartyEventEditor
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, ILog logger)
        {
            filters.Add(new HandleAllErrorAttribute(logger));
        }
    }
}
