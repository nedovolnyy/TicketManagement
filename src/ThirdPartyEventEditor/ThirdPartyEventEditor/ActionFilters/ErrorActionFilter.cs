using System.Web.Mvc;
using log4net;

namespace ThirdPartyEventEditor
{
    public class ErrorActionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var log = LogManager.GetLogger(typeof(ErrorActionFilter));
            filterContext.ExceptionHandled = true;
            log.Error(filterContext.Exception);
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
            };
        }
    }
}