using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OnlineBanking.Utilities
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary{
                     {"Controller","Account"},
                     {"Action","Login"}
                });
            }
        }
    }
}
