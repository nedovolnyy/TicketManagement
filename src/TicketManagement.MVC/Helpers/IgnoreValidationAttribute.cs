using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketManagement.MVC.Helpers
{
    [AttributeUsage(AttributeTargets.All)]
    public class IgnoreValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            foreach (var modelValue in modelState.Values)
            {
                modelValue.Errors.Clear();
            }
        }
    }
}
