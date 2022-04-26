using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.Filters
{
    public class LoggingFilter : Attribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
            context.HttpContext.Response.Cookies.Append("visited", "true");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine(context.HttpContext.Request.Path);
        }
    }
}
