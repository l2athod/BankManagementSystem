using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OnlineBanking.Utilities
{
    public class AuthenticationCustomer : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var role = context.HttpContext.Session.GetString("UserRole");
            if (context.HttpContext.Session.GetString("UserRole") != "Customer")
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary{
                     {"Controller","Admin"},
                     {"Action","Home"}
                });
            }
        }
    }
}
