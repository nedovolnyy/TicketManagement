using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace TicketManagement.WebUI.Services;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SerilogMvcLoggingAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var diagnosticContext = context.HttpContext.RequestServices.GetService<IDiagnosticContext>();
        diagnosticContext.Set("ActionName", context.ActionDescriptor.DisplayName);
        diagnosticContext.Set("ActionId", context.ActionDescriptor.Id);
    }
}